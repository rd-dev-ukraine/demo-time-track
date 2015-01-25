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
        public DatabaseInvoiceStorage(DataConnection db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            Db = db;
        }

        private DataConnection Db { get; set; }
        
        public void Save(Invoice invoice, List<InvoiceDetails> invoiceDetails)
        {
            Db.InsertOrReplace(invoice);
            Db.GetTable<InvoiceDetails>()
                     .Delete(d => d.InvoiceNum == invoice.InvoiceNum);

            foreach (var d in invoiceDetails)
            {
                d.InvoiceNum = invoice.InvoiceNum;
                Db.InsertOrReplace(d);
            }
        }
    }
}