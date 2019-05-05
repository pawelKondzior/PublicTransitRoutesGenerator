// -----------------------------------------------------------------------
//  <copyright file="XmlRepository.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------



namespace Magisterka.Data.Access.Repository
{
    using System.Xml.Linq;

    public abstract class XmlRepository 
    {
        #region Propetries

        /// <summary>
        /// Gets or sets the X document.
        /// </summary>
        /// <value>The X document.</value>
        /// <remarks></remarks>
        protected XDocument XDocument { get; set; }

        #endregion

        #region Constructor

        public XmlRepository(string path)
        {
            XDocument = XDocument.Load(path);
        }

        #endregion
    }
}