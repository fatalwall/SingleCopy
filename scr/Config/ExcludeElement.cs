/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using System;
using System.Configuration;

namespace SingleCopy.Config
{
    public class ExcludeElement : ConfigurationElement
    {
        public ExcludeElement() { }
        public ExcludeElement(string Folder, bool Recursive = false)
        {
            this.Folder = Folder;
            this.Recursive = Recursive;

        }

        [ConfigurationProperty("Folder", IsKey = true, IsRequired = true)]
        public string Folder
        {
            get { return base["Folder"] as string; }
            set { base["Folder"] = value; }
        }
        [ConfigurationProperty("Recursive", IsKey = false, IsRequired = true, DefaultValue=false)]
        public bool Recursive
        {
            get { return bool.Parse((string)base["Recursive"]); }
            set { base["Recursive"] = value; }
        }


        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            //get Attributes 
            for (int i = 0; i < reader.AttributeCount; i++)
            {
                reader.MoveToAttribute(i);
                try { this[reader.Name] = reader.Value; }
                catch (Exception e) { throw new ConfigurationErrorsException("XMLPart: ExcludeElement" + ", Attribute: " + reader.Name + ", Error: Unable to read in Value", e); }
            }

            //Get Text Content 
            reader.MoveToElement();
            this.Folder = reader.ReadElementContentAsString();
        }

        public bool Match(string Folder)
        {
            if (Recursive == true)
            {
                return Folder.ToUpper().StartsWith(this.Folder.ToUpper());
            }
            else
            {
                return Folder.Equals(this.Folder, StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}