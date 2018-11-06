using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YH.Language
{
    public class LanguageBase
    {
        #region *** Field ***

        const string rootName = "language"; //根名（必须此名，不可改变）
        public string lunguXMLFileName;     //XML文件名
        public XElement xf;                 //Linq：存储XML树型对象
        public string errMessage;           //存储返回的错误信息

        #endregion

        #region *** Constructor *** 

        public LanguageBase()
        {

        }

        public LanguageBase(string xmlfileName)
            : this()
        {
            lunguXMLFileName = xmlfileName;
        }

        #endregion

        #region *** Destructor ***

        ~LanguageBase()
        {
            // 这里是清理非托管资源的用户代码段
        }

        #endregion

        #region *** Property ***

        /// <summary>
        /// 语言XML文件名称
        /// </summary>
        public string XMLFileName
        {
            get;
            set;
        }

        #endregion

        #region *** Method *** 

        #region *** Public Method ***
        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="xmlfileName"></param>
        public void Load(string xmlfileName)
        {
            if (File.Exists(xmlfileName))
            {
                lunguXMLFileName = xmlfileName;
                xf = XElement.Load(lunguXMLFileName);
            }
        }

        /// <summary>
        /// 获取当前目录下所有的XML语言版本。[静态函数]
        /// </summary>
        /// <param name="pathXml">并反回数组列表</param>
        /// <returns></returns>
        public SortedList<string, string> getDirXml(string pathXml)
        {
            try
            {
                //当前目录下的所有语言版本的XML文件名及语言名称
                SortedList<string, string> languageList = new SortedList<string, string>();
                DirectoryInfo DirInfo = new DirectoryInfo(pathXml);

                var files = from f in DirInfo.EnumerateFiles()
                            where f.Extension.Contains(".xml")
                            select f;
                XElement xe;
                foreach (var f in files)
                {
                    //1、检测根名是否为languageConst
                    xe = XElement.Load(f.FullName);
                    if (xe.Name.LocalName.ToLower().Contains(rootName))
                    {
                        //2、检测根是否存在属性languageConst
                        var ats = from at in xe.Attributes()
                                  where at.Name.LocalName.ToLower().Contains(rootName)
                                  select at;
                        bool b = false;
                        foreach (var at in ats)
                        {
                            b = true;
                        }
                        //3、两个条件都成立时，添加此XML文档为语言类，否则忽略此XML文件
                        if (b)
                        {
                            languageList.Add(f.Name.Substring(0, f.Name.Length - 4) + "(" + xe.Attribute(rootName).Value + ")", f.FullName);
                        }
                    }
                }
                return languageList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="languName"></param>
        /// <returns></returns>
        public bool setRootName(string languName)
        {
            try
            {
                //设置根节点的属性值（即语言名称）
                xf.SetAttributeValue(rootName, languName);
                //保存
                xf.Save(lunguXMLFileName);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Linq建立XML文件（只有一个根节点的空白XML文件）
        /// </summary>
        /// <param name="fileName">建立的XML文件名。存储在当前目录，无需加路径</param>
        /// <returns></returns>
        public bool CreateXmlFile(string fileName)
        {
            try
            {
                //创建XML对象
                XDocument xDoc = new XDocument();
                //设置XML编码方式
                xDoc.Declaration = new XDeclaration("1.0", "utf-8", "");
                //创建一个根节点
                XElement root = new XElement(rootName);
                //设置根节点的属性及属性值（此值即为语言名称）
                XAttribute rootAttr = new XAttribute(rootName, rootName);
                root.Add(rootAttr);
                //将根节点加入到XML对象中
                xDoc.Add(root);

                //保存xml文件(存储在当前目录下)
                xDoc.Save(fileName);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 清空所有节点的语言名称值。应用在复制一份现有的XML语言
        /// </summary>
        /// <returns></returns>
        public bool ClearLanguValue()
        {
            try
            {
                var els = from el in xf.Elements()
                          select el;
                foreach (var el in els)
                {
                    el.Element("languName").Value = "";
                }
                xf.Save(lunguXMLFileName);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }



        /// <summary>
        /// 获取某节点的值(即此变量的语言名称)
        /// </summary>
        /// <param name="fVar">变量名</param>
        /// <returns>返回语言名称，不存在反回null</returns>
        public string getElementValue(string fVar)
        {
            try
            {
                var els = from el in xf.Elements()
                          where el.Name.LocalName.Equals(fVar, StringComparison.CurrentCultureIgnoreCase)
                          select el;
                if (els.Count() > 0)
                    return els.First().Element("languName").Value;
                else
                {
                    errMessage = null;
                    return null;
                }
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 添加新节点，即添加变量名
        /// </summary>
        /// <param name="fid">节点名即变量名</param>
        /// <param name="lang">语言名称</param>
        /// <param name="eg">示例名</param>
        /// <returns>成功返回True</returns>
        public bool AddElement(string fid, string lang, string eg)
        {
            try
            {
                //创建子节点
                XElement xele = new XElement(fid);
                xf.Add(xele);
                xele.SetElementValue("languName", lang);
                xele.SetElementValue("tagName", eg);

                xf.Save(lunguXMLFileName);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 修改节点的值（语言名称和示例；变量名不可修改。）
        /// </summary>
        /// <param name="fVar">变量名</param>
        /// <param name="lang">新的语言名称</param>
        /// <param name="eg">新的示例名称</param>
        /// <returns></returns>
        public bool UpdateElement(string fVar, string lang, string eg)
        {
            try
            {
                var els = from el in xf.Elements()
                          where el.Name.LocalName.Equals(fVar, StringComparison.CurrentCultureIgnoreCase)
                          select el;
                if (els.Count() > 0)
                {
                    els.First().Element("languName").Value = lang;
                    els.First().Element("tagName").Value = eg;
                    xf.Save(lunguXMLFileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 删除某节点的数据
        /// </summary>
        /// <param name="fVar">节点名即变量名</param>
        /// <returns></returns>
        public bool DelElement(string fVar)
        {
            try
            {
                var els = from el in xf.Elements()
                          where el.Name.LocalName.Equals(fVar, StringComparison.CurrentCultureIgnoreCase)
                          select el;
                if (els.Count() > 0)
                    els.First().Remove();
                xf.Save(lunguXMLFileName);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取XML所有节点的信息，并反回节点信息表
        /// </summary>
        /// <returns>返回节点信息表DataTable</returns>
        public DataTable getXElements()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("fid", Type.GetType("System.String"));
                dt.Columns.Add("langu", Type.GetType("System.String"));
                dt.Columns.Add("tag", Type.GetType("System.String"));
                DataRow dr = null;

                var ce = from el in xf.Elements()
                         select el;
                foreach (var el in ce)
                {
                    dr = dt.NewRow();
                    dr["fid"] = el.Name;
                    dr["langu"] = el.Element("languName").Value;
                    dr["tag"] = el.Element("tagName").Value;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return null;
            }
        }

        #endregion

        #region *** Private Mothod ***

        #endregion

        #endregion
    }
}
