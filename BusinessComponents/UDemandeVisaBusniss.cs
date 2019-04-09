using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessComponents
{
    public class UDemandeVisaBusniss:IDisposable
    {
        DemandeVisaDBAccess visa = new DemandeVisaDBAccess();
        private bool disposed = false;

        public void addVisaRequest(DemandeVisa demandeVisa)
        {
            visa.addRequestVisa(demandeVisa);
        }
        public IList<DemandeVisa> showVisaRequest()
        {
            return visa.getRequestVisa().ToList();
        }
        public DemandeVisa deleteVisaRequest(int? id)
        {
            return visa.deleteRequestVisa(id);
        }
        public bool deleteVisaRequestConfirmed(int? id)
        {
            return visa.deleteRequestVisaConfirmed(id);
        }
        public DemandeVisa detailsVisaRequest(int? id)
        {
            return visa.detailsRequestVisa(id);
        }
        public DemandeVisa editVisaRequest(int? id)
        {
            return visa.editRequestVisa(id);
        }
        public void editVisaRequest(DemandeVisa demandeVisa)
        {
            visa.editRequestVisa(demandeVisa);
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
                    visa.Dispose();
                    visa = null;
                }
                disposed = true;
            }
        }
    }
}
