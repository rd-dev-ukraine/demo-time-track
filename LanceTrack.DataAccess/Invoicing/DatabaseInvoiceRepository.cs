using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Server.Dependencies.Invoicing;
using LanceTrack.Server.Dependencies.Projects;

namespace LanceTrack.Server.DataAccess.Invoicing
{
    public class DatabaseInvoiceRepository : IInvoiceRepository
    {
        private readonly IProjectRepository _projectRepository;

        public DatabaseInvoiceRepository(DbManager dbManager, IProjectRepository projectRepository)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");
            if(projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            DbManager = dbManager;
            _projectRepository = projectRepository;
            
        }

        private DbManager DbManager { get; set; }

        public Invoice GetByNumber(string invoiceNumber, int userId)
        {
            return Invoices(userId).SingleOrDefault(r => r.InvoiceNum == invoiceNumber);
        }

        public List<InvoiceDetails> Details(string invoiceNumber, int userId)
        {
            return InvoiceDetails(userId).Where(d => d.InvoiceNum == invoiceNumber)
                                         .ToList();
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
            return DbManager.GetTable<Invoice>()
                            .Join(_projectRepository.BillableProjects(userId), i => i.ProjectId, p => p.Id, (i,p) => i);
        }

        private IQueryable<InvoiceDetails> InvoiceDetails(int userId)
        {
            return DbManager.GetTable<InvoiceDetails>()
                            .Join(Invoices(userId), i => i.InvoiceNum, i => i.InvoiceNum, (d, i) => d);
        }
    }
}