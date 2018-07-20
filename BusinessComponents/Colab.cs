using System;
using DataAccess;
using Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WebForms;
using PagedList;

namespace BusinessComponents
{
    public class Colab
    {
        ColaborateurDBAccess colab = new ColaborateurDBAccess();
        public void ajouterColab(Colaborateur colaborateur)
        {
            colab.addColaborateur(colaborateur);
        }
        public PagedList.IPagedList<Colaborateur> afficherColab(string sortOrder,string searchString,string currentFilter,int? page,int pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var colaborateurs = from c in colab.showColaborateurs() select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                colaborateurs = colaborateurs.Where(c => c.Nom.Contains(searchString) || c.Prenom.Contains(searchString) || c.Email.Contains(searchString)||c.Equipe.Contains(searchString)||c.Poste.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nom_desc":
                    colaborateurs = colaborateurs.OrderByDescending(c => c.Nom);
                    break;
                case "prenom":
                    colaborateurs = colaborateurs.OrderBy(c => c.Prenom);
                    break;
                case "prenom-desc":
                    colaborateurs = colaborateurs.OrderByDescending(c => c.Prenom);
                    break;
                case "email":
                    colaborateurs = colaborateurs.OrderBy(c => c.Email);
                    break;
                case "email-desc":
                    colaborateurs = colaborateurs.OrderByDescending(c => c.Email);
                    break;
                case "equipe":
                    colaborateurs = colaborateurs.OrderBy(c => c.Equipe);
                    break;
                case "equipe-desc":
                    colaborateurs = colaborateurs.OrderByDescending(c => c.Equipe);
                    break;
                case "poste":
                    colaborateurs = colaborateurs.OrderBy(c => c.Poste);
                    break;
                case "poste-desc":
                    colaborateurs = colaborateurs.OrderByDescending(c => c.Poste);
                    break;
                default:
                    colaborateurs = colaborateurs.OrderBy(c => c.Nom);
                    break;
            }
            int pageNumber = (page ?? 1);

            return colaborateurs.ToPagedList(pageNumber, pageSize);
        }

        public bool supprimerColaborateurConfirmer(int? id)
        {
            return colab.deleteColaborateurConfirmed(id);
        }
        public Colaborateur supprimerColaborateur(int? id)
        {
            return colab.deleteColaborateurs(id);
        }
        public Colaborateur detaillerColaborateur(int? id)
        {
            return colab.detailsColaborateur(id);
        }
        public Colaborateur modifierColborateur(int? id)
        {
            return colab.editColaborateur(id);
        }
        public void modifierColaborateur(Colaborateur colaborateur)
        {
            colab.editColaborateur(colaborateur);
        }
        /***Export PDf,EXCEL,Word,IMG*****/
        public String FileExtension(string exportType)
        {
            string fileNameExtension;
            if (exportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            if (exportType == "Word")
            {
                fileNameExtension = "docx";
            }
            if (exportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
           else
                fileNameExtension = "docx";

            return fileNameExtension;
        }
        public byte[] ExporterColaborateur(string exportType, String path)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = path;
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "SqliTarkettDataSet";
            reportDataSource.Value = colab.showColaborateurs().ToList();
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
    }
}
