﻿using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Cqrs.ProjectTime.Commands;
using LanceTrack.Server.Dependencies.Invoicing;

namespace LanceTrack.Server.Invoicing
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICqrs _cqrs;
        private readonly UserAccount _currentUser;
        private readonly IInvoiceRepository _invoiceRepository; 

        public InvoiceService(ICqrs cqrs, UserAccount currentUser, IInvoiceRepository invoiceRepository)
        {
            if (cqrs == null)
                throw new ArgumentNullException("cqrs");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (invoiceRepository == null)
                throw new ArgumentNullException("invoiceRepository");

            _cqrs = cqrs;
            _currentUser = currentUser;
            _invoiceRepository = invoiceRepository;
        }

        public IEnumerable<Invoice> MyPendingInvoices()
        {
            return _invoiceRepository.UserPendingInvoices(_currentUser.Id).ToList();
        }

        public IEnumerable<Invoice> Archive()
        {
            return _invoiceRepository.UserArchiveInvoices(_currentUser.Id).ToList();
        }

        public Invoice Get(string number)
        {
            if (String.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException("number");

            return _invoiceRepository.GetByNumber(number, _currentUser.Id);
        }

        public List<InvoiceDetails> Details(string invoiceNumber)
        {
            if(String.IsNullOrWhiteSpace(invoiceNumber))
                throw new ArgumentNullException("invoiceNumber");

            return _invoiceRepository.Details(invoiceNumber, _currentUser.Id)
                                     .ToList();
        }

        public List<InvoiceRecalculationResult> RecalculateInvoiceInfo(int projectId, List<InvoiceUserRequest> invoiceUserRequest)
        {
            var recalculateInvocieInfoCommand = new RecalculateInvoiceInfoCommand
            {
                ProjectId = projectId,
                ByUserId = _currentUser.Id,
                InvoiceUserRequest = (invoiceUserRequest ?? new List<InvoiceUserRequest>()).ToList()
            };

            _cqrs.Execute(recalculateInvocieInfoCommand);

            return recalculateInvocieInfoCommand.Result;
        }

        public string BillProject(int projectId, List<InvoiceUserRequest> invoiceUserRequest)
        {
            var billProjectCommand = new BillProjectCommand
            {
                ProjectId = projectId,
                ByUserId = _currentUser.Id,
                InvoiceUserRequest = invoiceUserRequest.Where(i => i.Hours > 0).ToList()
            };

            _cqrs.Execute(billProjectCommand);

            return billProjectCommand.Result;
        }
    }
}