using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.UserAccounts;
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
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            DbManager = dbManager;
            _projectRepository = projectRepository;

        }

        private DbManager DbManager { get; set; }

        public InvoiceInfo GetByNumber(string invoiceNumber, int userId)
        {
            return Invoices(userId).SingleOrDefault(r => r.InvoiceNum == invoiceNumber);
        }

        public List<InvoiceDetails> Details(string invoiceNumber, int userId)
        {
            return InvoiceDetails(userId).Where(d => d.InvoiceNum == invoiceNumber)
                                         .ToList();
        }

        public IEnumerable<InvoiceInfo> UserArchiveInvoices(int userId)
        {
            return Invoices(userId).Where(i => i.IsPaid || i.IsCancelled).OrderByDescending(i => i.At);
        }

        public IEnumerable<InvoiceInfo> UserPendingInvoices(int userId)
        {
            return Invoices(userId).Where(i => !i.IsPaid && !i.IsCancelled).OrderByDescending(i => i.At);
        }

        private IQueryable<InvoiceInfo> Invoices(int userId)
        {
            return DbManager.GetTable<Invoice>()
                            .Join(_projectRepository.BillableProjects(userId),
                                  i => i.ProjectId, p => p.Id,
                                  (i, p) => new { Invoice = i, Project = p })
                            .Join(DbManager.GetTable<UserAccount>(),
                                  a => a.Invoice.BilledByUserId,
                                  u => u.Id,
                                  (a, u) => new InvoiceInfo
                                  {
                                      At = a.Invoice.At,
                                      BilledByUserDisplayName = u.DisplayName,
                                      BilledByUserId = a.Invoice.BilledByUserId,
                                      Hours = a.Invoice.Hours,
                                      InvoiceNum = a.Invoice.InvoiceNum,
                                      IsCancelled = a.Invoice.IsCancelled,
                                      IsPaid = a.Invoice.IsPaid,
                                      ProjectId = a.Invoice.ProjectId,
                                      ProjectTitle = a.Project.Name,
                                      ReceivedSum = a.Invoice.ReceivedSum,
                                      Sum = a.Invoice.Sum
                                  });
        }

        private IQueryable<InvoiceDetails> InvoiceDetails(int userId)
        {
            return DbManager.GetTable<InvoiceDetails>()
                            .Join(Invoices(userId), i => i.InvoiceNum, i => i.InvoiceNum, (d, i) => d);
        }
    }
}