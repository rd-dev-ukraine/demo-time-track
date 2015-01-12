using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Server.Dependencies.Invoicing;

namespace LanceTrack.DataAccess.Invoicing
{
    public class DatabaseInvoiceRepository : IInvoiceRepository
    {
        public DatabaseInvoiceRepository(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public Invoice GetByNumber(string invoiceNumber, int userId)
        {
            return Invoices(userId).SingleOrDefault(r => r.InvoiceNum == invoiceNumber);
        }

        public IEnumerable<Invoice> UserArchiveInvoices(int userId)
        {
            return Invoices(userId).Where(i => i.IsPaid);
        }

        public IEnumerable<Invoice> UserPendingInvoices(int userId)
        {
            return Invoices(userId).Where(i => !i.IsPaid);
        }

        private IQueryable<Invoice> Invoices(int userId)
        {
            return DbManager.GetTable<Invoice>().Where(i => i.UserId == userId);
        }
    }
}