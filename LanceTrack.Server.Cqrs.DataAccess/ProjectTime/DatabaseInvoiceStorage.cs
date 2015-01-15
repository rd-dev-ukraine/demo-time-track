using System;
using System.Collections.Generic;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class DatabaseInvoiceStorage : IInvoiceStorage
    {
        public DatabaseInvoiceStorage(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public void Save(Invoice invoice, List<InvoiceDetails> invoiceDetails)
        {
            DbManager.InsertOrReplace(invoice);
            DbManager.GetTable<InvoiceDetails>().Delete(d => d.InvoiceNum == invoice.InvoiceNum);

            foreach (var d in invoiceDetails)
            {
                d.InvoiceNum = invoice.InvoiceNum;
                DbManager.InsertOrReplace(d);
            }
        }
    }
}