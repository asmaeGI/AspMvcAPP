using System;
using DataAccess;
using Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WebForms;
using PagedList;
using System.Web.Mvc;
using System.Web.Helpers;

namespace BusinessComponents
{
    public class UColaborateurBusniss:IDisposable
    {
        CollaborateurDBAccess colab = new CollaborateurDBAccess();
        private bool disposed = false;

        public void addCollaborator(Colaborateur colaborateur)
        {
            colab.addCollaborator(colaborateur);
        }
        public SelectList getCollaboratorsNames(int? id)
        {
            return colab.getCollaboratorsNames(id);
        }
        public IList<Colaborateur> getCollaborators()
        {
            return colab.getCollaborators();
        }
        public Colaborateur getCollaborator(int? id)
        {
            return colab.getCollaboratorsById(id);
        }
        public IList<Colaborateur> showCollaborators()
        {
            return colab.getCollaborators().ToList();
        }
        public bool deleteCollaboratorConfirmed(int? id)
        {
            return colab.deleteCollaboratorConfirmed(id);
        }
        public Colaborateur deleteCollaborator(int? id)
        {
            return colab.deleteCollaborators(id);
        }
        public Colaborateur detailsCollaborator(int? id)
        {
            return colab.detailsCollaborator(id);
        }
        public Colaborateur editCollaboratorById(int? id)
        {
            return colab.editCollaborator(id);
        }
        public void editCollaborator(Colaborateur colaborateur)
        {
            colab.editCollaborator(colaborateur);
        }
        public Colaborateur getCollaborator(Colaborateur colaborateur)
        {
           return colab.findCollaborator(colaborateur);
        }
        /***Export PDf,EXCEL*****/
        public String FileExtension(string exportType)
        {
            string fileNameExtension;
            if (exportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            if (exportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
           else
                fileNameExtension = "pdf";

            return fileNameExtension;
        }
        public byte[] ExportCollaborator(string exportType, String path)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = path;
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "SqliTarkettDataSet";
            reportDataSource.Value = colab.getCollaborators().ToList();
            localreport.DataSources.Add(reportDataSource);
            string fileNameExtension = this.FileExtension(exportType);
            string reportType = exportType;
            string mimeType;
            string encoding;
            string[] streams;
            Warning[] warning;
            byte[] renderedByte;
            renderedByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warning);
            return renderedByte;
        }

        //************ Email *************//
public bool sendMail(string email,string objet,string message)
        {
            try
            {
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = true;
                //Send email  
                WebMail.Send(to: email, subject: objet, body: message, isBodyHtml: true);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    colab.Dispose();
                    colab = null;
                }
                disposed = true;
            }
        }

    }
}
