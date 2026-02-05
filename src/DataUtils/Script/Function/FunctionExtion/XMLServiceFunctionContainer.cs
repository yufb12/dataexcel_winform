using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Xml;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class XMLFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "XMLServiceFunction";
        public const string Function_Description = "XML";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        } 

        public XMLFunctionContainer()  
        {

            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "XMLInit";
            model.Description = @"XMLInit";
            model.Eg = @"XMLInit(""<xml>aaaa</xml>"")";
            model.Function = XMLInit;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "XMLAttributes";
            model.Description = @"获取节点属性集合 XMLAttributes(doc)";
            model.Eg = @"XMLAttributes(doc)";
            model.Function = XMLAttributes;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "XMLAttribute";
            model.Description = @"获取指定节点属性 XMLAttributes(doc)";
            model.Eg = @"XMLAttribute(doc,1)";
            model.Function = XMLAttribute;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "XMLChildNodes";
            model.Description = @"获取子节点集合 XMLChildNodes(doc)";
            model.Eg = @"XMLChildNodes(doc,1)";
            model.Function = XMLChildNodes;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "XMLChildNode";
            model.Description = @"获取指定子节点 XMLChildNode(doc)";
            model.Eg = @"XMLChildNode(doc,1)";
            model.Function = XMLChildNode;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "XMLInnerText";
            model.Description = @"获取节点文本 XMLInnerText(doc)";
            model.Eg = @"XMLInnerText(doc,1)";
            model.Function = XMLInnerText;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "XMLInnerXml";
            model.Description = @"获取节点XML XMLInnerXml(doc)";
            model.Eg = @"XMLInnerXml(doc,1)";
            model.Function = XMLInnerXml;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "XMLValue";
            model.Description = @"获取节点值 XMLValue(doc)";
            model.Eg = @"XMLValue(doc,1)";
            model.Function = XMLValue;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "XMLName";
            model.Description = @"获取节点值 XMLValue(node)";
            model.Eg = @"XMLName(node,1)";
            model.Function = XMLName;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "XMLText";
            model.Description = @"获取XML文本 XMLText(doc)";
            model.Eg = @"XMLText(doc)";
            model.Function = XMLText;
            MethodList.Add(model);
        }
        public virtual object XMLText(params object[] args)
        {
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            return doc.ToString();
        }
        public virtual object XMLInit(params object[] args)
        { 
            string text = base.GetTextValue(1, args);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(text); 
            return doc;
        }
        public virtual object XMLAttributes(params object[] args)
        { 
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            int index = base.GetIntValue(2, args);
            XmlAttributeCollection value = doc.Attributes;
            return value;
        }
        public virtual object XMLAttribute(params object[] args)
        {
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            int index = base.GetIntValue(2, args);
            int valuetype = base.GetIntValue(3, args);
            XmlAttributeCollection attributes = null;
            if (doc != null)
            {
                attributes = doc.Attributes;
            }
            else
            {
                attributes = base.GetArgIndex(1, args) as XmlAttributeCollection;
            }
            XmlAttribute xmlAttribute= attributes[index];
            switch (valuetype)
            {
                case 1:
                    return xmlAttribute.InnerText;
                case 2:
                    return xmlAttribute.InnerXml;
                case 3:
                    return xmlAttribute.Value;
                case 4:
                    return xmlAttribute.Name;
                case 5:
                    return xmlAttribute.LocalName;
                case 6:
                    return xmlAttribute.NamespaceURI;
                case 7:
                    return (int)xmlAttribute.NodeType;
                case 8:
                    return xmlAttribute.OuterXml;
                case 9:
                    return xmlAttribute.Prefix;
                case 10:
                    return xmlAttribute.PreviousSibling;
                case 11:
                    return xmlAttribute.NextSibling;
                case 12:
                    return xmlAttribute.LastChild;
                default:
                    break;
            }
            return xmlAttribute;
        }
        public virtual object XMLChildNodes(params object[] args)
        {
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode; 
            XmlNodeList value = doc.ChildNodes;
            return value;
        }
        public virtual object XMLChildNode(params object[] args)
        { 

            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            int index = base.GetIntValue(2, args);
            int valuetype = base.GetIntValue(3, args);
            XmlNodeList attributes = null;
            if (doc != null)
            {
                attributes = doc.ChildNodes;
            }
            else
            {
                attributes = base.GetArgIndex(1, args) as XmlNodeList;
            }
            XmlNode xmlAttribute = attributes[index];
            switch (valuetype)
            {
                case 1:
                    return xmlAttribute.InnerText;
                case 2:
                    return xmlAttribute.InnerXml;
                case 3:
                    return xmlAttribute.Value;
                case 4:
                    return xmlAttribute.Name;
                case 5:
                    return xmlAttribute.LocalName;
                case 6:
                    return xmlAttribute.NamespaceURI;
                case 7:
                    return (int)xmlAttribute.NodeType;
                case 8:
                    return xmlAttribute.OuterXml;
                case 9:
                    return xmlAttribute.Prefix;
                case 10:
                    return xmlAttribute.PreviousSibling;
                case 11:
                    return xmlAttribute.NextSibling;
                case 12:
                    return xmlAttribute.LastChild;
                default:
                    break;
            }
            return xmlAttribute;
        }
        public virtual object XMLInnerText(params object[] args)
        {
            string value = string.Empty;
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            string text = base.GetTextValue(2, args);
            if (!string.IsNullOrWhiteSpace(text))
            {
                doc.InnerText = value;
                return Feng.Utils.Constants.YES;
            }
            else
            {
                value = doc.InnerText;
            }
            return value;
        }
        public virtual object XMLInnerXml(params object[] args)
        {
            string value = string.Empty;
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            string text = base.GetTextValue(2, args);
            if (!string.IsNullOrWhiteSpace(text))
            {
                doc.InnerXml = value;
                return Feng.Utils.Constants.YES;
            }
            else
            {
                value = doc.InnerXml;
            } 
            return value;
        }
        public virtual object XMLValue(params object[] args)
        {
            string value = string.Empty;
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            value = doc.Value;
            return value;
        }
        public virtual object XMLName(params object[] args)
        {
            string value = string.Empty;
            XmlNode doc = base.GetArgIndex(1, args) as XmlNode;
            value = doc.Name;
            return value;
        }
    }
}
