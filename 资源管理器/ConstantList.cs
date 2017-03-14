using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace 资源管理器
{
   public class ConstantList
    {
        public const int MyComputer = 2;      //我的电脑
        public const int ClosedFolder = 1;    //文件夹关闭
        public const int OpenFolder = 0;      //文件夹打开
      //public const int FixedDrive = 3;      //磁盘盘符
        public const int MyDocuments = 1;     //我的文档
        public const int recycle = 4;
        public string path;                   //获取桌面路径
        public string SearhPathFileName;        //搜索路径文件名

        public bool SearchFileName;          //判断搜索文件名是否存在

        public string ToFilePath;            //转到文件路径

        public SqlConnection conLocation = new SqlConnection();//连接到本地路径

        public  SqlCommand comLocation = new SqlCommand();//在本地使用T-SQL语句

        public List<string> tablenames = new List<string>(); //在数据库中查找用于迭代的表名

        public string[] restrictionValues;//本地数据源架构

        public DataTable schemaInformation;//将查询的数据表
        public SqlDataReader dr;//检测本地是否使用T-SQL语句
    }
}
