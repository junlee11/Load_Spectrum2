using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Load_Spectrum2
{
    public class Global_FunSet : INotifyPropertyChanged
    {
        //필드
        private Dictionary<string, string> _source_EQP_dict = new Dictionary<string, string>();
        private DataTable _dt_eqp_info = new DataTable();
        private Dictionary<string, string> _source_mguide_LineURI = new Dictionary<string, string>();
        private string _default_path;
        private string _mguide_id;
        private string _mguide_pw;

        //Global 변수
        static public int ftpTimeout = 10000;
        
        //코딩
        static public string info_uri = @"FTP://192.168.0.103:21/S5/Load_Spectrum2/InfoEQP/Setting_EQP.csv";
        static public string info_id = "LS_C";
        static public string info_pw = "LS_C";

        //실제
        //static public string info_uri = @"FTP://10.125.66.247:21/S5/Load_Spectrum2/InfoEQP/Setting_EQP.csv";
        //static public string info_id = "LS_H";
        //static public string info_pw = "KKK";

        static public string info_t_path = @"C:\Load_Spectrum2\Setting_EQP.csv";        
        static public string radmin_pw = "fmfm1234";
        static public Dictionary<string, string> ETL_Sim_pathDic = new Dictionary<string, string>();
        static public Dictionary<string, string> ETL_Sim_LocalPath = new Dictionary<string, string>();


        public Global_FunSet()
        {
            _dt_eqp_info = Global_FunSet.GetCSVData(@"C:\Load_Spectrum2\Setting_EQP.csv", 1);
            if (_dt_eqp_info == null) _dt_eqp_info = Global_FunSet.GetCSVData("Load_Spectrum2.Resources.Setting_EQP.csv", 2);

            foreach (DataRow row in _dt_eqp_info.Rows)
            {
                _source_EQP_dict.Add(row["EQPID"].ToString(), row["IP"].ToString());
            }

            _source_mguide_LineURI["S1"] = "FTP://12.21.214.175:21/g/MGUIDE-IF/KFBK";
            _source_mguide_LineURI["S4"] = "FTP://12.21.214.175:21/g/MGUIDE-IF/KFBY";
            _source_mguide_LineURI["S3"] = "FTP://12.21.214.175:21/g/MGUIDE-IF/KFBN";
            _source_mguide_LineURI["S5"] = "FTP://12.21.214.175:21/g/MGUIDE-IF/PFBF";

            _default_path = @"C:\Load_Spectrum2";

            //실제
            _mguide_id = "mguide";
            _mguide_pw = "mguide1!";
        }

        //Properties        
        public Dictionary<string, string> source_EQP_dict
        {
            get { return _source_EQP_dict; }
            set { this._source_EQP_dict = value; }
        }

        public DataTable dt_eqp_info
        {
            get { return _dt_eqp_info; }
            set { this._dt_eqp_info = value; }
        }

        public Dictionary<string, string> source_mguide_LineURI
        {
            get { return _source_mguide_LineURI; }
            set { this._source_mguide_LineURI = value; }
        }
        public string default_path
        {
            get { return _default_path; }
            set { this._default_path = value; }
        }
        public string mguide_id
        {
            get { return _mguide_id; }
            set { this._mguide_id = value; }
        }
        public string mguide_pw
        {
            get { return _mguide_pw; }
            set { this._mguide_pw = value; }
        }

        //
        public static int FTPOneFileDown(string uri, string t_path, string id, string pw)
        {
            try
            {
                //최초 다운로드
                var request = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));
                request.Credentials = new NetworkCredential(id, pw);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Timeout = 10000;     //5초안에 접속 안되면 종료
                FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();

                Stream sourceStream = resFtp.GetResponseStream();
                FileStream targetFileStream = new FileStream(t_path, FileMode.Create, FileAccess.Write);
                byte[] bufferByteArray = new byte[1024];

                while (true)
                {
                    int byteCount = sourceStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                    if (byteCount == 0)
                    {
                        break;
                    }
                    targetFileStream.Write(bufferByteArray, 0, byteCount);
                }
                targetFileStream.Close();
                sourceStream.Close();

                //다운로드 성공시 1 반환
                return 1;
            }
            catch
            {
                //다운로드 실패시 -1 반환
                return -1;
            }
        }

        public static int FTPOneFileUpload(string uri, string s_path, string id, string pw)
        {
            try
            {
                //최초 다운로드
                var request = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));
                request.Credentials = new NetworkCredential(id, pw);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream sourceFileStream = new FileStream(s_path, FileMode.Open, FileAccess.Read);
                Stream targetStream = request.GetRequestStream();
                byte[] bufferByteArray = new byte[1024];
                while (true)
                {
                    int byteCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                    if (byteCount == 0)
                    {
                        break;
                    }
                    targetStream.Write(bufferByteArray, 0, byteCount);
                }
                targetStream.Close();
                sourceFileStream.Close();

                //다운로드 성공시 1 반환
                return 1;
            }
            catch
            {
                //다운로드 실패시 -1 반환
                return -1;
            }

        }

        public static DataTable GetCSVData(string str_path, int resource_flag)            //2이면 임베디드 리소스
        {
            try
            {
                StreamReader file;
                if (resource_flag == 1) file = new StreamReader(str_path);
                else
                {
                    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(str_path);
                    file = new StreamReader(stream);
                }


                DataTable table = new DataTable();
                var flag_dtcolumn = 0;

                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] data = line.Split(',');

                    if (flag_dtcolumn == 0)
                    {
                        foreach (string s in data)
                        {
                            table.Columns.Add(s);
                        }
                    }
                    else
                    {
                        table.Rows.Add(data.ToArray());
                    }

                    flag_dtcolumn++;

                }

                return table;
            }
            catch
            {
                return null;
            }

        }

        public static bool IsExistDirectory(string Directory, string id, string pw)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(new Uri(Directory.Replace("#", "%23")));
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(id, pw);
                request.Timeout = Global_FunSet.ftpTimeout;
                using (request.GetResponse())
                {
                    request.GetResponse().Close();
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        public static bool IsExistFile(string Directory, string id, string pw)
        {
            bool isExist = true;
            try
            {
                Directory = Directory.Replace("#", "%23");
                var request = (FtpWebRequest)WebRequest.Create(new Uri(Directory.Replace("#", "%23")));
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(id, pw);
                request.Timeout = Global_FunSet.ftpTimeout;
                var resFTP = (FtpWebResponse)request.GetResponse();

                if (resFTP.StatusCode == System.Net.FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    isExist = false;
                }
                resFTP.Close();
            }
            //파일 사이즈를 가져오다가 오류 발생하면 파일 없다고(false) 반환
            catch
            {
                isExist = false;
            }

            return isExist;
        }

        public static bool IsFTPConnect(string ip, string id, string pw)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ip);
                request.Credentials = new NetworkCredential(id, pw);
                request.Timeout = Global_FunSet.ftpTimeout;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse resp = request.GetResponse();

                resp.Close();
                return true;

            }
            catch (WebException)
            {
                return false;
            }
        }

        public static void MakeFTPFolder(string ftp_uri, string id, string pw, string txt_path)
        {
            //폴더 없어서 폴더 생성
            //폴더 생성
            string total_path = "";
            string[] txt_path_part = txt_path.Split('/');
            string tar_path = "";

            int i = 0;

            foreach (string txt_part in txt_path_part)
            {
                if (i == 0) total_path = txt_part;
                else total_path = total_path + "/" + txt_part;

                try
                {
                    tar_path = ftp_uri + "/" + total_path;
                    var request = (FtpWebRequest)WebRequest.Create(new Uri(tar_path.Replace("#", "%23")));
                    request.Credentials = new NetworkCredential(id, pw);
                    request.UseBinary = true;
                    request.UsePassive = true;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Timeout = Global_FunSet.ftpTimeout;
                    FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();
                    resFtp.Close();

                }
                catch { }

                i++;

            }



        }

        public static long ReturnLocalFileFolderCount(string[] src_items, long result_cnt)
        {
            foreach (string Drag_Item in src_items)
            {
                string _path = Drag_Item;
                FileAttributes chkAtt = File.GetAttributes(_path);
                if ((chkAtt & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // 디렉토리일 경우, 재귀
                    result_cnt = result_cnt + 1;
                    string[] tmpPath = Directory.GetDirectories(_path);
                    result_cnt = Global_FunSet.ReturnLocalFileFolderCount(tmpPath, result_cnt);

                    //파일일경우
                    string[] tmpFiles = Directory.GetFiles(_path);
                    result_cnt = Global_FunSet.ReturnLocalFileFolderCount(tmpFiles, result_cnt);
                }
                else
                {
                    // 파일 일 경우
                    result_cnt = result_cnt + 1;
                }
            }
            return result_cnt;
        }

        public static string ReturnFileSizeStr(long raw_fileSize)
        {
            if (raw_fileSize == 0)
            {
                return "";
            }
            else if (raw_fileSize <= 1024)
            {
                return "1KB";
            }
            else if (raw_fileSize <= 1024 * 1024)
            {
                raw_fileSize = raw_fileSize / (1024);
                return raw_fileSize.ToString() + "KB";
            }
            else if (raw_fileSize <= 1024 * 1024 * 1024)
            {
                raw_fileSize = raw_fileSize / (1024 * 1024);
                return raw_fileSize.ToString() + "MB";
            }
            else
            {
                raw_fileSize = raw_fileSize / (1024 * 1024 * 1024);
                return raw_fileSize.ToString() + "GB";
            }
        }

        public static string ReturnIconPathStr(string fileExetension)
        {
            fileExetension = fileExetension.ToUpper();
            if (fileExetension.Contains("TXT"))
            {
                //return "Resources/icon_notepad.ico";
                return "pack://application:,,,/Resources/icon_notepad.ico";
            }
            else if (fileExetension.Contains("폴더"))
            {
                //return "Resources/icon_folder.png";
                return "pack://application:,,,/Resources/icon_folder.png";
            }
            else if (fileExetension.Contains(".XLS"))
            {
                //return "Resources/icon_excel.png";
                return "pack://application:,,,/Resources/icon_excel.png";
            }
            else if (fileExetension.Contains(".CSV"))
            {
                //return "Resources/icon_csv.png";
                return "pack://application:,,,/Resources/icon_csv.png";
            }
            else if (fileExetension.Contains(".TIF") || fileExetension.Contains(".GIF") || fileExetension.Contains(".JPG") || fileExetension.Contains(".BMP") || fileExetension.Contains(".PNG"))
            {
                //return "Resources/icon_image.png";
                return "pack://application:,,,/Resources/icon_image.png";
            }
            else if (fileExetension.Contains(".PDF"))
            {
                //return "Resources/icon_pdf.png";
                return "pack://application:,,,/Resources/icon_pdf.png";
            }
            else if (fileExetension.Contains(".DOC"))
            {
                //return "Resources/icon_word.png";
                return "pack://application:,,,/Resources/icon_word.png";
            }
            else if (fileExetension.Contains(".ZIP"))
            {
                //return "Resources/icon_zip.png";
                return "pack://application:,,,/Resources/icon_zip.png";
            }
            else if (fileExetension.Contains(".PY"))
            {
                //return "Resources/icon_py.png";
                return "pack://application:,,,/Resources/icon_py.png";
            }
            else if (fileExetension.Contains(".HTM"))
            {
                //return "Resources/icon_htm.png";
                return "pack://application:,,,/Resources/icon_htm.png";
            }
            else if (fileExetension.Contains(".DAT"))
            {
                //return "Resources/icon_dat.png";
                return "pack://application:,,,/Resources/icon_dat.png";
            }
            else if (fileExetension.Contains(".PPT"))
            {
                //return "Resources/icon_ppt.png";
                return "pack://application:,,,/Resources/icon_ppt.png";
            }
            else
            {
                //return "Resources/icon_file.ico";
                return "pack://application:,,,/Resources/icon_file.ico";
            }
        }

        //EQP Items의 모든 파일, 폴더 개수 반환
        public static long ReturnEQPFileFolderCount(string[] src_items, string id, string pw, long result_cnt)
        {
            foreach (string Drag_Item in src_items)
            {
                string _path = Drag_Item;
                if (_path.Substring(_path.Length - 1, 1) == "/")
                {
                    _path = _path.Substring(0, _path.Length - 1);
                }
                if (!Global_FunSet.IsExistFile(_path, id, pw))
                {
                    result_cnt = result_cnt + 1;

                    //파일,폴더일 경우, 재귀

                    //FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(_path);
                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(_path.Replace("#", "%23")));
                    ftpReq.Credentials = new NetworkCredential(id, pw);
                    ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                    ftpReq.Timeout = Global_FunSet.ftpTimeout;
                    FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                    StreamReader reader;
                    reader = new StreamReader(resFtp.GetResponseStream());

                    string strData;
                    strData = reader.ReadToEnd();

                    string[] tmpPath = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < tmpPath.Length; i++)
                    {
                        //tmpPath[i] = this.line_set1_eqp.Text + "/" + tmpPath[i];
                        tmpPath[i] = Drag_Item + tmpPath[i].Substring(tmpPath[i].LastIndexOf("/"), tmpPath[i].Length - tmpPath[i].LastIndexOf("/"));
                    }
                    resFtp.Close();
                    result_cnt = Global_FunSet.ReturnEQPFileFolderCount(tmpPath, id, pw, result_cnt);

                    //파일일경우
                    //string[] tmpFiles = Directory.GetFiles(_path);
                    //ReturnLocalFileFolderCount(tmpFiles);
                }
                else
                {
                    result_cnt++;
                }
            }
            return result_cnt;
        }

        public static bool IsImageExtension(string extension)
        {
            bool Isimage = false;
            if (extension.ToUpper().Contains("BMP")) Isimage = true;
            else if (extension.ToUpper().Contains("JPG") || extension.ToUpper().Contains("JPEG")) Isimage = true;
            else if (extension.ToUpper().Contains("GIF")) Isimage = true;
            else if (extension.ToUpper().Contains("PNG")) Isimage = true;
            else if (extension.ToUpper().Contains("TIF")) Isimage = true;
            else if (extension.ToUpper().Contains("EXIF")) Isimage = true;

            return Isimage;
        }

        public static void DownImage(string src_path, string tar_path, string id, string pw)
        {
            try
            {
                if (!Global_FunSet.IsExistFile(src_path, id, pw)) return;

                var request = (FtpWebRequest)WebRequest.Create(new Uri(src_path.Replace("#", "%23")));
                request.Credentials = new NetworkCredential(id, pw);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Timeout = Global_FunSet.ftpTimeout;
                FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();

                Stream sourceStream = resFtp.GetResponseStream();
                FileStream targetFileStream = new FileStream(tar_path, FileMode.Create, FileAccess.Write);
                byte[] bufferByteArray = new byte[1024];

                while (true)
                {
                    int byteCount = sourceStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                    if (byteCount == 0)
                    {
                        break;
                    }
                    targetFileStream.Write(bufferByteArray, 0, byteCount);
                }
                targetFileStream.Close();
                sourceStream.Close();
            }
            catch { }
        }

        public static DataTable DataGridtoDataTable(DataGrid dg)
        {
            try
            {
                DataTable dt = new DataTable();

                for (int i = 0; i <= dg.Columns.Count - 1; i++)
                {
                    dt.Columns.Add(dg.Columns[i].Header.ToString(), typeof(string));
                }
                DataRow Row;

                for (int i = 0; i <= dg.Items.Count - 1; i++)
                {
                    Row = dt.NewRow();

                    for (int k = 0; k <= dg.Columns.Count - 1; k++)
                    {
                        //string t = gettabelcell(i, k, dg);
                        Row[dg.Columns[k].Header.ToString()] = gettabelcell(i, k, dg);
                        //Debug.WriteLine(t);

                    }
                    dt.Rows.Add(Row);
                }

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터그리드를 데이터테이블을 변환하는데 실패했습니다." + "\n" + ex.Message);
                return null;
            }
            

        }

        //row x, column y
        public static String gettabelcell(int x, int y, DataGrid dg)
        {
            try
            {
                DataGridCellInfo cellInfo = new DataGridCellInfo(dg.Items[x], dg.Columns[y]);
                DataGridCell cell =  Global_FunSet.TryToFindGridCell(dg, cellInfo, x, y);
                TextBlock t = (TextBlock)cell.Content;
                string myString = t.Text;
                //Debug.WriteLine(myString);
                //String myString = dg.GetCell(x, y).ToString();

                return myString;
                //if (myString.Contains(":"))
                //{
                //    String[] s = myString.Split(':');
                //    String item = s[1];
                //    return item.Trim();
                //}
                //else
                //{
                //    return "";
                //}
            }
            catch
            {
                try
                {
                    DataGridCellInfo cellInfo = new DataGridCellInfo(dg.Items[x], dg.Columns[y]);
                    DataGridCell cell = Global_FunSet.TryToFindGridCell(dg, cellInfo, x, y);
                    TextBlock t = (TextBlock)cell.Content;
                    string myString = t.Text;

                    return myString;
                }
                catch
                {
                    return "";
                }
                
            }
        }

        //보조
        public static DataGridCell TryToFindGridCell(DataGrid grid, DataGridCellInfo cellInfo, int x, int y)
        {
            DataGridCell result = null;
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(cellInfo.Item);
            if (row == null)
            {
                try
                {
                    grid.UpdateLayout();
                    try
                    {
                        grid.ScrollIntoView(grid.Items[x + 10]);
                    }
                    catch
                    {
                        grid.ScrollIntoView(grid.Items[x]);
                    }                    
                    cellInfo = new DataGridCellInfo(grid.Items[x], grid.Columns[y]);
                    row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(cellInfo.Item);
                }
                catch { }
                
            }

            if (row != null)
            {
                int columnIndex = grid.Columns.IndexOf(cellInfo.Column);
                if (columnIndex > -1)
                {
                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                    result = presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
                }
            }
            return result;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static bool DatatableToCSV(DataTable dtDataTable, string strFilePath)
        {
            try
            {
                StreamWriter sw = new StreamWriter(strFilePath, false);
                //headers    
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    sw.Write(dtDataTable.Columns[i]);
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                int br_flag = 0;
                //this.print_DataTable(dtDataTable);
                foreach (DataRow dr in dtDataTable.Rows)
                {
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[0].ToString();
                            if (i == 0 && value == "")
                            {
                                br_flag = 1;
                                break;
                            }
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                Debug.WriteLine(value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    if (br_flag == 1) break;

                    sw.Write(sw.NewLine);
                }
                sw.Close();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("데이터테이블을 csv로 저장할 수 없습니다." + "\n" + ex.Message);
                return false;
            }
            
        }

        public static void print_DataTable(DataTable dt, int header = 1)
        {
            // header = 1 -> Print header, header = 0 -> No print header
            int m = dt.Columns.Count; // number of column
            int n = dt.Rows.Count; // number of row

            string[] line = new string[m];
            string[] result = new string[n + header];
            if (header == 1)
            {
                //result[0] = "\t" + String.Join("\t", names(dt));
                List<String> list_col = new List<String>();
                foreach (DataColumn col in dt.Columns)
                {
                    list_col.Add(col.ColumnName);
                }
                result[0] = "\t" + String.Join("\t", list_col);

            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    line[j] = dt.Rows[i][j].ToString();
                result[i + header] = i + "\t" + String.Join("\t", line);
            }
            foreach (var e in result)
            {
                Debug.WriteLine(e);
            }
        }

        public static DataTable MkClipboardToDataTable()
        {
            string s = Clipboard.GetText();
            DataTable dt = new DataTable();

            string[] lines = s.Split('\r');

            for (int i = 0; i <= lines.Count() - 1; i++)
            {
                lines[i] = lines[i].Trim('\n');
            }

            string[] fields;
            int row = 0;
            int col = 0;

            //열생성
            for (int i = 0; i <= lines[0].Split('\t').Count() - 1; i++)
            {
                dt.Columns.Add("col_" + i.ToString(), typeof(string));
            }

            foreach (string item in lines)
            {
                if (item == "") continue;
                dt.Rows.Add();
                fields = item.Split('\t');
                foreach (string f in fields)
                {
                    dt.Rows[row][col] = f;
                    col++;
                }
                row++;
                col = 0;
            }
            return dt;            
        }

        public static bool CheckDataGridOverlap(DataGrid dg, int col)
        {
            //중복 있으면 1, 없으면 0            
            List<string> str_list = new List<string>();
            for (int i = 0; i <= dg.Items.Count - 1; i++)
            {
                string item = gettabelcell(i, col, dg);
                if (item != "") str_list.Add(gettabelcell(i, col, dg));
            }
            
            if (str_list.Count != str_list.Distinct().Count())
            {
                return true;
            }
            return false;
        }

        public static List<string> ReturnSimilarPath(string src_path, string ip, string id, string pw, int path_num)
        {
            List<string> list_rst = new List<string>();
            int fail_flag = 0;
            string[] part_path = src_path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            for (int num = 0; num < path_num; num++)
            {
                //split
                
                string rst_path = "";
                string back_uri = "";

                int cnt = 0;
                foreach (string part in part_path)
                {
                    if (cnt == 0) rst_path = part;
                    else rst_path = rst_path + "/" + part;

                    string uri = String.Format("ftp://{0}:21/{1}", ip, rst_path);

                    if (!IsExistDirectory(uri, id, pw))
                    {
                        //1. 날짜 형식이면
                        string pattern = @"^20[0-9]{2}-[0-1][0-9]-[0-3][0-9]T[0-2][0-9]-[0-6][0-9]-[0-9]{2}";
                        Regex regex = new Regex(pattern);
                        if (regex.IsMatch(part))
                        {
                            string recent_date = ReturnFTPRecentPath(back_uri + "/", id, pw, path_num - num - 1);
                            if (recent_date == "fail")
                            {
                                fail_flag = 1;
                                break;
                            }
                                
                            rst_path = rst_path.Replace(part, recent_date);
                        }
                        else if (part == "RD")
                        {
                            string new_part = "ResultsData";
                            rst_path = rst_path.Replace(part, new_part);
                        }
                        else if (part == "ResultsData")
                        {
                            string new_part = "RD";
                            rst_path = rst_path.Replace(part, new_part);
                        }
                        else if ((back_uri.Contains("RD") || back_uri.Contains("ResultsData")) && part.Contains(".csv"))
                        {
                            //file은 항상 한개만 폴더에 있으므로 num에 0을 넣어줌
                            string recent_file = ReturnFTPRecentPath(back_uri + "/", id, pw, 0);
                            if (recent_file == "fail")
                            {
                                fail_flag = 1;
                                break;
                            }
                            rst_path = rst_path.Replace(part, recent_file);
                        }
                        else
                        {                            
                            return null;
                        }
                    }
                    back_uri = uri;
                    cnt++;
                }
                if (fail_flag != 1) list_rst.Add(rst_path);
            }

            return list_rst;
        }

        public static string ReturnSimilarImagePath(string src_path, string ip, string id, string pw)
        {
            string rst = "";
            int fail_flag = 0;
            string[] part_path = src_path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            //split
            string rst_path = "";
            string back_uri = "";

            int cnt = 0;
            foreach (string part in part_path)
            {
                if (cnt == 0) rst_path = part;
                else rst_path = rst_path + "/" + part;

                string uri = String.Format("ftp://{0}:21/{1}", ip, rst_path);

                if (!IsExistDirectory(uri, id, pw))
                {
                    //1. 날짜 형식이면
                    string pattern = @"^20[0-9]{2}-[0-1][0-9]-[0-3][0-9]T[0-2][0-9]-[0-6][0-9]-[0-9]{2}";
                    Regex regex = new Regex(pattern);
                    if (regex.IsMatch(part))
                    {
                        string recent_date = ReturnFTPRecentPath(back_uri + "/", id, pw, 0);
                        if (recent_date == "fail")
                        {
                            fail_flag = 1;
                            break;
                        }

                        rst_path = rst_path.Replace(part, recent_date);
                    }
                    else if (part == "RD")
                    {
                        string new_part = "ResultsData";
                        rst_path = rst_path.Replace(part, new_part);
                    }
                    else if (part == "ResultsData")
                    {
                        string new_part = "RD";
                        rst_path = rst_path.Replace(part, new_part);
                    }
                    else if (part == "AI")
                    {
                        string new_part = "AlignImage";
                        rst_path = rst_path.Replace(part, new_part);
                    }
                    else if (part == "MeasurementImage")
                    {
                        string new_part = "MI";
                        rst_path = rst_path.Replace(part, new_part);
                    }
                    else if (part == "MI")
                    {
                        string new_part = "MeasurementImage";
                        rst_path = rst_path.Replace(part, new_part);
                    }
                    else if (IsImageExtension(part))
                    {                        
                        string new_part = ReturnFTPImgPath(back_uri + "/", id, pw, uri);
                        if (new_part == "") rst_path = "";
                        else rst_path = rst_path.Replace(part, new_part);
                    }
                    else
                    {
                        return "";
                    }
                }
                back_uri = uri;
                cnt++;
            }

            return rst_path;
        }

        public static string ReturnFTPImgPath(string back_uri, string id, string pw, string uri)
        {
            string rst = "";
            try
            {
                
                string[] new_arr = uri.Split('/');
                string new_uri = new_arr[new_arr.Count() - 1];
                new_uri = new_uri.Substring(0, new_uri.IndexOf("__"));

                FtpWebRequest reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(back_uri.Replace("#", "%23")));
                reqFtp.Credentials = new NetworkCredential(id, pw);
                reqFtp.Timeout = ftpTimeout;
                reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                StreamReader reader;
                reader = new StreamReader(resFtp.GetResponseStream());
                string strData;
                strData = reader.ReadToEnd();
                string[] filesInDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Array.Sort(filesInDirectory);
                resFtp.Close();

                foreach (string s in filesInDirectory)
                {
                    if (s.Contains(new_uri)) rst = s;
                }
                
            }
            catch
            {
                rst = "";
            }
            return rst;
        }

        public static string ReturnFTPRecentPath(string uri, string ip, string pw, int num)
        {
            FtpWebRequest reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));
            reqFtp.Credentials = new NetworkCredential(ip, pw);
            reqFtp.Timeout = ftpTimeout;
            reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
            StreamReader reader;
            reader = new StreamReader(resFtp.GetResponseStream());
            string strData;
            strData = reader.ReadToEnd();
            string[] filesInDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(filesInDirectory);                        
            resFtp.Close();

            //가장 마지막 (최신) 폴더명 반환
            try
            {
                return filesInDirectory[filesInDirectory.Length - 1 - num];
            }
            catch
            {
                return "fail";
            }            
        }

        public static bool chk_list_in(List<string>list, string find_e)
        {
            foreach (var ele in list)
            {
                if (ele.Contains(find_e)) return true;
            }
            return false;
        }

        public static bool chk_list_Allin(List<string> list, string find_e)
        {
            foreach (var ele in list)
            {
                if (ele != find_e) return false;
            }
            return true;
        }

        public static void print_list(List<String> lst)
        {

            foreach (var e in lst)
            {
                Debug.WriteLine(e);
            }

        }

        public static int MkExcelFrom_RNRSrc(List<NewModel.Model> ItemLayer, List<string> Wafer, List<string> Ineg, Dictionary<string, string> local_path)
        {

            //엑셀 객체 생성
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws_All = null;
            Excel.Worksheet ws_IN = null;
            Excel.Worksheet ws_EDGE = null;
            Dictionary<string, string> dic_rawInfo = new Dictionary<string, string>();            
            int num_eqp = 0;            

            try
            {
                foreach (var itemlayer in ItemLayer)
                {
                    foreach (string w in Wafer)
                    {
                        foreach (var entry in local_path)
                        {
                            string select_eqp = entry.Key;
                            string select_path = entry.Value;
                            string txt = File.ReadAllText(select_path);
                            string[] txt_part = txt.Split(new string[] { "WAFER ID" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in txt_part)
                            {
                                string temp = part.Substring(1, part.IndexOf(",", 2) - 1);   //맨앞 쉼표 제거
                                string w_num = temp.Substring(temp.IndexOf("_") + 1, 2);

                                string cast_item = itemlayer.strRNR_Item.ToString();
                                if (part.Contains("TEST ID," + cast_item) && Int32.Parse(w) == Int32.Parse(w_num))
                                {
                                    try
                                    {
                                        dic_rawInfo.Add(String.Format("{0}_{1}_{2}", select_eqp, cast_item, w), part);
                                    }
                                    catch
                                    {
                                        //중복 무시
                                    }

                                }
                            }
                        }
                    }
                }

                //EQP, Wafer, Layer 순서 정의하는 Dic만들기
                Dictionary<string, int> dic_EQPOrder = new Dictionary<string, int>();
                dic_EQPOrder = ReturnEQPOrderDic(local_path);
                num_eqp = dic_EQPOrder.Count;

                Dictionary<string, int> dic_WaferOrder = new Dictionary<string, int>();
                dic_WaferOrder = ReturnListOrderDic(Wafer);

                Dictionary<string, int> dic_Layer = new Dictionary<string, int>();
                int cnt_temp = 0;
                foreach (var entry_layer in ItemLayer)
                {
                    string str = string.Format("{0}_{1}", entry_layer.strRNR_Item, entry_layer.strRNR_Layer);
                    dic_Layer.Add(str, cnt_temp);
                    cnt_temp++;
                }

                excelApp = new Excel.Application();
                //excelApp.Visible = true;
                wb = excelApp.Workbooks.Add();
                int pass_fail_all = 0;
                int pass_fail_in = 0;
                int pass_fail_eg = 0;

                if (chk_list_in(Ineg, "All"))
                {
                    ws_All = wb.Worksheets.Add(After: wb.Sheets[wb.Sheets.Count]) as Excel.Worksheet;
                    ws_All.Name = "All";
                    pass_fail_all = InputToExcel(ws_All, 1, "end", ItemLayer, dic_rawInfo, dic_EQPOrder, dic_WaferOrder, dic_Layer);
                }
                if (chk_list_in(Ineg, "INNER"))
                {
                    ws_IN = wb.Worksheets.Add(After: wb.Sheets[wb.Sheets.Count]) as Excel.Worksheet;
                    ws_IN.Name = "INNER";
                    pass_fail_in = InputToExcel(ws_IN, 1, "13", ItemLayer, dic_rawInfo, dic_EQPOrder, dic_WaferOrder, dic_Layer);
                }
                if (chk_list_in(Ineg, "EDGE"))
                {
                    ws_EDGE = wb.Worksheets.Add(After: wb.Sheets[wb.Sheets.Count]) as Excel.Worksheet;
                    ws_EDGE.Name = "EDGE";
                    pass_fail_eg = InputToExcel(ws_EDGE, 14, "end", ItemLayer, dic_rawInfo, dic_EQPOrder, dic_WaferOrder, dic_Layer);
                }

                if (pass_fail_all == -1) MessageBox.Show("엑셀이 정상적으로 생성되지 않았습니다. : All");
                if (pass_fail_in == -1) MessageBox.Show("엑셀이 정상적으로 생성되지 않았습니다. : INNER");
                if (pass_fail_eg == -1) MessageBox.Show("엑셀이 정상적으로 생성되지 않았습니다. : EDGE");

                // 엑셀파일 저장
                string filename = Return_ExcelFileName(dic_rawInfo.First().Value);

                DirectoryInfo di = new DirectoryInfo(@"C:\Load_Spectrum2\RNR");
                if (di.Exists == false)
                {
                    di.Create();
                }

                wb.SaveAs(@"C:\Load_Spectrum2\RNR\" + filename);
                wb.Close(true);
                excelApp.Quit();
                return 1;
            }
            catch
            {
                //실패하면 -1 반환
                return -1;
            }            
            finally
            {
                // Clean up
                ReleaseExcelObject(ws_All);
                ReleaseExcelObject(ws_IN);
                ReleaseExcelObject(ws_EDGE);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }
            
            
        }

        public static List<string> ReturnSumStrCommaDel(List<string> src)
        {
            //Sum 콤마 제거 4회 반복
            List<string> rst = FunSumCommaDel(src);
            rst = FunSumCommaDel(rst);
            rst = FunSumCommaDel(rst);
            rst = FunSumCommaDel(rst);

            return rst;
        }

        private static List<string> FunSumCommaDel (List<string> src)
        {
            List<string> temp = new List<string>();            
            int idx1 = 0;
            int idx2 = 0;
            int sum_flag = 0;
            int cnt = 0;

            //temp 생성
            foreach (string str in src)
            {
                string str_upper = str.ToUpper();
                if (str_upper.Contains("SUMT") && !str_upper.Contains(")"))
                {
                    idx1 = cnt;
                    sum_flag = 1;
                }

                if (sum_flag == 1)
                {
                    string sum = "";
                    if (str_upper.Contains("SUMT") && !str_upper.Contains(")"))
                    {
                        sum = str_upper.Substring(str_upper.IndexOf("SUMT"), 6);
                        temp.Add(sum);
                    }
                    else if (str_upper.Contains(")") && cnt > idx1 && sum_flag == 1)
                    {
                        sum = str_upper.Substring(0, str_upper.IndexOf(")") + 1);
                        temp.Add(sum);
                    }
                    else
                    {
                        temp.Add(str);
                    }                    
                }

                if (str_upper.Contains(")") && cnt > idx1 && sum_flag == 1)
                {
                    idx2 = cnt;
                    sum_flag = 0;
                    break;
                }
                cnt++;
            }

            //제거
            if (idx1 != 0 && idx2 != 0)
            {
                for (int i = 0; i < idx2 - idx1 + 1; i++)
                {
                    src.RemoveAt(idx1);
                }

                //Sum Str 생성
                string sum_rst = "";
                int cnt2 = 0;
                foreach (string t in temp)
                {
                    if (cnt2 == 0) sum_rst = t;
                    else sum_rst = sum_rst + ":" + t;
                    cnt2++;
                }
                src.Insert(idx1, sum_rst);               
            }            

            return src;
        }

        private static string Return_ExcelFileName(string txt)
        {
            //ex. 220621_F0A1.1_[RECIPE].xlsx
            string temp = txt.Substring(1, txt.IndexOf(",", 2) - 1);       //맨앞의 쉼표 제외                                
            string LotID = temp.Substring(0, temp.IndexOf("_"));            
            temp = txt.Substring(txt.IndexOf("RECIPE") + 7, txt.Length - txt.IndexOf("RECIPE") - 9);
            string recipe = temp.Substring(0, temp.IndexOf(",")).Replace("\\", "_");
            string today = DateTime.Now.ToString("yyMMdd");
            return string.Format("{0}_{1}_{2}.xlsx", today, LotID, recipe);
        }

        private static int InputToExcel(Excel.Worksheet ws, int num_start, string str_end, List<NewModel.Model>ItemLayer, Dictionary<string, string>dic_rawInfo, Dictionary<string, int>dic_EQPOrder, Dictionary<string, int>dic_WaferOrder, Dictionary<string, int>dic_Layer)
        {
            int num_eqp = dic_EQPOrder.Count;
            int num_end = 0;

            //All Sheet
            try
            {
                if (ws != null)
                {
                    //초기 설정
                    ws.Activate();
                    int cnt = 0;
                    int col_interval = 0;
                    int row_interval = 0;
                    int col_layer = 0;

                    int num_site = 0;

                    //Set 내부 순서 : 설비(시간순) - Wafer - Item
                    //Layer - Set 순서로 반복
                    foreach (var entry_layer in ItemLayer)      //선택한 아이템Layer 반복
                    {
                        //레이어 바뀔때 레이어 초기화
                        int add_row = 2;
                        int add_col = 2;                        

                        string layer = entry_layer.strRNR_Layer;

                        foreach (var entry in dic_rawInfo)      //raw 데이터를 시간 순서로 반복
                        {                            
                            //Set의 Item이 entry_layer의 아이템과 같은 조건에서 진행 (같은필름조건)
                            if (entry.Key.Split('_')[1] == entry_layer.strRNR_Item.ToString())
                            {
                                //Element
                                string eqp = entry.Key.Split('_')[0];
                                string test_id = entry.Key.Split('_')[1];   //1,2,...
                                string wafer_num = entry.Key.Split('_')[2];
                                string wafer_id = entry.Value.Substring(1, entry.Value.IndexOf(",", 2) - 1);       //맨앞의 쉼표 제외                                

                                //add_point 정의
                                if (dic_EQPOrder[eqp] == 0)
                                {                                    
                                    add_row = 1 + (dic_WaferOrder[wafer_num] * row_interval);
                                    add_col = 1 + (dic_Layer[string.Format("{0}_{1}", test_id, layer)] * col_interval);
                                    cnt = 0;
                                }

                                //엑셀 기입////////////////////////////////////////////////////////////////////////

                                if (cnt == 0)
                                {
                                    (col_layer, num_site) = Excel_Return_ColLayer_NumSite(entry.Value, layer);

                                    //All의 경우 : 1, end (+ NPW)
                                    //INNER의 경우 : 1, 13
                                    //EDGE의 경우 : 14, end

                                    //num 범위 정하기
                                    if (str_end == "end")
                                    {
                                        num_end = num_site;
                                    }
                                    else
                                    {
                                        num_end = Int32.Parse(str_end);
                                    }

                                    if (num_start >= num_end) return -1;
                                    num_site = num_end - num_start + 1;

                                    row_interval = num_site + 9;
                                    if (num_eqp < 4) col_interval = 6;
                                    else col_interval = num_eqp + 3;

                                    //Recipe
                                    ws.Cells[add_row, add_col].Value = "RECIPE";
                                    string temp = entry.Value.Substring(entry.Value.IndexOf("RECIPE") + 7, entry.Value.Length - entry.Value.IndexOf("RECIPE") - 9);
                                    string recipe = temp.Substring(0, temp.IndexOf(","));
                                    ws.Cells[add_row + 1, add_col].Value = recipe;

                                    //WaferID
                                    ws.Cells[add_row, add_col + 1].Value = "WAFER ID";
                                    ws.Cells[add_row + 1, add_col + 1].Value = wafer_id;

                                    //TestID
                                    ws.Cells[add_row, add_col + 2].Value = "TEST ID";
                                    ws.Cells[add_row + 1, add_col + 2].Value = test_id;

                                    //LayerID
                                    ws.Cells[add_row, add_col + 3].Value = "LAYER";
                                    ws.Cells[add_row + 1, add_col + 3].Value = layer;

                                    //Site #
                                    ws.Cells[add_row + 3, add_col].Value = "Site #";

                                    //매칭 안되는 설비 표시
                                    //dic_EQPOrder, dic_rawInfo
                                    int no_math_num = 0;
                                    string no_match_eqp = ReturnNoMatchEQP(dic_rawInfo, dic_EQPOrder);
                                    if (no_match_eqp != "")
                                    {
                                        ws.Cells[add_row + 2, add_col].Value = "측정 Wafer없는 설비 : " + no_match_eqp;
                                        string[] temp_s = no_match_eqp.Split(',');
                                        no_math_num = temp_s.Count();
                                    }                                        

                                    //Chart
                                    //Excel.ChartObjects chartobjs = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                                    //Excel.ChartObject chartobj = chartobjs.Add(((add_col - 1)/col_interval) * 320 + 10, ((add_row - 1)/row_interval) * ((num_end - num_start + 6) * 20)  + 70, 250, 200);
                                    //Excel.Chart xlChart = chartobj.Chart;
                                    //var startCell = (Excel.Range)ws.Cells[add_row + 3, add_col + 1];
                                    //var endCell = (Excel.Range)ws.Cells[add_row + 3 + (num_end - num_start + 1), add_col + dic_EQPOrder.Count - no_math_num];
                                    //Excel.Range range = ws.get_Range(startCell, endCell);   // range                                    
                                    //xlChart.SetSourceData(range, Type.Missing);
                                    //xlChart.ChartType = Excel.XlChartType.xlLineMarkers;                                    
                                    //xlChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom;
                                    //xlChart.ChartTitle.Font.Size = 10;                                    
                                    //var yAxis = (Excel.Axis)xlChart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
                                    //yAxis.MajorGridlines.Border.Color = System.Drawing.ColorTranslator.FromHtml("#DCDCDC");
                                    //yAxis.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;                                    
                                    //var Xaxis = (Excel.Axis)xlChart.Axes(Excel.XlAxisType.xlCategory);                                    
                                    //Xaxis.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                    //foreach (Excel.Series xlSeries in xlChart.FullSeriesCollection())
                                    //{
                                    //    xlSeries.MarkerStyle = Excel.XlMarkerStyle.xlMarkerStyleCircle;
                                    //}
                                }
                                //eqp
                                ws.Cells[add_row + 3, add_col + 1 + dic_EQPOrder[eqp]].Value = eqp;

                                List<double> Site_Val = new List<double>();
                                Site_Val = Excel_Return_SiteVal(entry.Value, col_layer, num_start, num_end);
                                for (int i = 0; i < num_site; i++)
                                {
                                    ws.Cells[add_row + 3 + (i + 1), add_col].Value = (i + num_start).ToString();
                                    ws.Cells[add_row + 3 + (i + 1), add_col + 1 + dic_EQPOrder[eqp]].Value = Site_Val[i];
                                }

                                //수식
                                ws.Cells[add_row + 3 + num_site + 1, add_col].Value = "AVG";
                                ws.Cells[add_row + 3 + num_site + 1, add_col + dic_EQPOrder[eqp] + 1].FormulaR1C1 = string.Format("=AVERAGE(R[-{0}]C:R[-1]C)", num_site);
                                ws.Cells[add_row + 3 + num_site + 2, add_col].Value = "RANGE";
                                ws.Cells[add_row + 3 + num_site + 2, add_col + dic_EQPOrder[eqp] + 1].FormulaR1C1 = string.Format("=MAX(R[-{0}]C:R[-2]C) - MIN(R[-{0}]C:R[-2]C)", num_site + 1);
                                ws.Cells[add_row + 3 + num_site + 3, add_col].Value = "STD";
                                ws.Cells[add_row + 3 + num_site + 3, add_col + dic_EQPOrder[eqp] + 1].FormulaR1C1 = string.Format("=STDEV(R[-{0}]C:R[-3]C)", num_site + 2);

                                ///////////////////////////////////////////////////////////////////////////////////////////////
                                string back_eqp = entry.Key.Split('_')[0];
                                cnt++;
                            }
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            
        }

        private static string ReturnNoMatchEQP(Dictionary<string, string> raw, Dictionary<string, int> eqp)
        {
            List<string> list_rst = new List<string>();
            List<string> raw_keylist = new List<string>();
            string rst = "";
            int flag = 0;
            //raw dic key에 eqp가 속하는지 검사

            foreach (string key in raw.Keys)
            {
                string real_key = key.Substring(0, key.IndexOf("_"));
                raw_keylist.Add(real_key);
            }

            //Debug.WriteLine(raw_keylist[0]);
            
            List<string> eqp_keyList = new List<string>(eqp.Keys);

            foreach (var e in eqp_keyList)
            {
                if (!chk_list_in(raw_keylist, e))
                {
                    list_rst.Add(e);
                }
            }            

            if (list_rst.Count == 0) rst = "";
            else
            {
                int cnt = 0;
                foreach (var ele in list_rst)
                {
                    if (cnt == 0) rst = ele;
                    else
                    {
                        rst = rst + " , " + ele;
                    }
                }
            }

            return rst;
        }

        //static 변수는 선언만 해도 메모리가 할당된다. 
        //일반 변수는 객체가 생성시에만 메모리가 초기화되지만 해당 객체의 생성여부와 상관없이 static은 메모리에 계속 남아있는다.
        //private static : 메모리에 static 공간 선언, 객체 선언 없이 같은 class내에서 접근가능
        //public static : 메모리에 static 공간 선언, 객체 선언 없이 외부의 class에서도 접근가능
        private static Dictionary<string, int> ReturnListOrderDic(List<string> wafer)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            int cnt = 0;
            foreach (var ele in wafer)
            {
                dic.Add(ele, cnt);
                cnt++;
            }

            return dic;
        }

        private static Dictionary<string,int> ReturnEQPOrderDic(Dictionary<string, string> local_path)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            int cnt = 0;
            foreach (var entry in local_path)
            {
                dic.Add(entry.Key, cnt);
                cnt++;
            }

            return dic;
        }

        private static (int, int) Excel_Return_ColLayer_NumSite(string one_set, string layer)
        {
            //한개의 set을 받아서 col_layer, site개수 반환
            int col_layer = 0;
            int num_site = 0;

            string[] row_part = one_set.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            //한행씩 반복
            foreach (string part in row_part)
            {
                //한행을 다시 스플릿해서 첫번째 요소가 숫자이면 site
                int n = 0;
                if (Int32.TryParse(part.Split(',')[0], out n))
                {
                    //숫자이면 
                    num_site++;
                }

                //Site, Layer열 검색
                if (part.Split(',')[0] == "Site #")
                {
                    List<string> New_part = Global_FunSet.ReturnSumStrCommaDel(part.Split(',').ToList());
                    //string[] layer_part = part.Split(',');

                    int cnt = 0;
                    foreach(string p in New_part)
                    {
                        if (p == layer) col_layer = cnt;

                        cnt++;
                    }
                }
            }
            

            return (col_layer, num_site);
        }

        private static List<double> Excel_Return_SiteVal(string one_set, int col_layer, int num_start, int num_end)
        {
            List<double> rst = new List<double>();

            string[] row_part = one_set.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            //한행씩 반복
            foreach (string part in row_part)
            {
                //한행을 다시 스플릿해서 첫번째 요소가 숫자이면 site
                int n = 0;
                if (Int32.TryParse(part.Split(',')[0], out n))
                {
                    //숫자이면서 site number가 num_start ~ num_end이어야 함
                    int n2 = Int32.Parse(part.Split(',')[0]);

                    if (n2 >= num_start && n2 <= num_end)
                    {
                        string[] temp = part.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        rst.Add(double.Parse(temp[col_layer]));
                    }
                }
            }
            return rst;
        }



        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
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
