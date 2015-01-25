using System;
using System.Collections.Generic;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LinqToDB;
using LinqToDB.Data;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class DatabaseInvoiceStorage : IInvoiceStorage
    {
        public DatabaseInvoiceStorage(DataConnection dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DataConnection DbManager { get; set; }
        
        public void Save(Invoice invoice, List<InvoiceDetails> invoiceDetails)
        {
            DbManager.InsertOrReplace(invoice);
            DbManager.GetTable<InvoiceDetails>()
                     .Delete(d => d.InvoiceNum == invoice.InvoiceNum);

            foreach (var d in invoiceDetails)
            {
                d.InvoiceNum = invoice.InvoiceNum;
                DbManager.InsertOrReplace(d);
            }
        }
    }
}