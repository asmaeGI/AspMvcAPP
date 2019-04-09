using DataAccess;
using Entities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessComponents
{
    public class DeplacementBusniss:IDisposable
    {
        DeplacementDBAccess dep = new DeplacementDBAccess();
        CollaborateurDBAccess colab = new CollaborateurDBAccess();
        private bool disposed = false;

        public IList<Deplacement> showDisplacement()
        {
            return dep.getDisplacements().ToList();
        }
        public IList<Deplacement> getDeplacements()
        {
            return dep.getDisplacements();
        }
        public void addDisplacement(Deplacement deplacement, int?[] IdU)
        {
            dep.addDisplacement(deplacement, IdU);
        }
        public Deplacement deleteDisplacement(int? id)
        {
            return dep.deleteDisplacement(id);
        }
        public bool deleteDisplacementConfirmed(int? id)
        {
            return dep.deleteDisplacementConfirmed(id);
        }
        public Deplacement detailsDisplacement(int? id)
        {
            return dep.detailsDisplacement(id);
        }
        public Deplacement editDisplacementById(int? id)
        {
            return dep.editDisplacement(id);
        }
        public void editDisplacement(Deplacement deplacement, int?[] IdU)
        {
            dep.editDisplacement(deplacement, IdU);
        }
        public IList<Colaborateur> CollaboratorsSuggest()
        {
            return dep.CollaboratorsSuggestion();
        }
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
        public byte[] ExportCollaborators(string exportType, String path)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = path;
            localreport.SubreportProcessing += new SubreportProcessingEventHandler(
                              delegate (object sender, SubreportProcessingEventArgs e)
                              
                              {
                                  ReportDataSource reportSource = new ReportDataSource();
                                  reportSource.Name = "ColaborateurDataSet";
                                  reportSource.Value = colab.getCollaborators().ToList();
                                  e.DataSources.Add(reportSource);
                              }
    );
            localreport.Refresh();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DeplacementDataSet";
            reportDataSource.Value = dep.getDisplacements().ToList();
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
                    dep.Dispose();
                    dep = null;
                }
                disposed = true;
            }
        }

    }
}
