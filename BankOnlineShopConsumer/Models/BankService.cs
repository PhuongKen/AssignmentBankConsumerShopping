﻿using BankOnlineShopConsumer.BankReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOnlineShopConsumer.Models
{
    public class BankService
    {
        BankClient bank = new BankClient();

        List<PartnerAccount> partnerList = new List<PartnerAccount>();

        public bool LoginPartnerAccount(PartnerAccount partner)
        {
            var partnerNew = new BankReference.PartnerAccount()
            {
                partnerAccount1 = partner.partnerAccount,
                password = partner.password
            };
            if (bank.LoginPartnerAccount(partnerNew))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Transaction(Transaction transaction)
        {
            var transactioNew = new BankReference.Transaction()
            {
                name = transaction.name,
                amount = transaction.amount,
                content = transaction.content,
                senderAccountNumber = transaction.senderAccountNumber,
                receiverAccountNumber = transaction.receiverAccountNumber,
                billId = transaction.billId,
                status = transaction.status,
                type = transaction.type,
                createAt = transaction.createAt,
                updateAt = transaction.updateAt
            };
            if (bank.AddTransaction(transactioNew))
            {
                return true;
            }
            {
                return false;
            }
        }

        public PartnerAccount Find(long id)
        {
            var find = bank.GetPartnerById(id);
            PartnerAccount partner = new PartnerAccount();
            partner.accountNumber = find.accountNumber;
            return partner;
        }

    }
}