using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Load_Spectrum2.NewViewModel
{    
    public class ViewModel_InfoEQP : INotifyPropertyChanged
    {
        //global function set
        public static Global_FunSet gfun;

        public ViewModel_InfoEQP()
        {
            gfun = new Global_FunSet();

            source_InfoEQP = new List<NewModel.Model>();

            foreach (DataRow row in gfun.dt_eqp_info.Rows)
            {
                source_InfoEQP.Add(new NewModel.Model() { 
                    strEQP_grid_LINE = row["LINE"].ToString(), 
                    strEQP_grid_EQPTYPE = row["EQPTYPE"].ToString(),
                    strEQP_grid_EQPMODEL = row["EQPMODEL"].ToString(),
                    strEQP_grid_ZONE = row["ZONE"].ToString(),
                    strEQP_grid_EQPID = row["EQPID"].ToString(),
                    strEQP_grid_IP = row["IP"].ToString()
                });
            }
            Debug.WriteLine("good");
        }

        private List<NewModel.Model> _source_InfoEQP;
        public List<NewModel.Model> source_InfoEQP
        {
            get { return _source_InfoEQP; }
            set { _source_InfoEQP = value; OnPropertyChanged("source_InfoEQP"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
