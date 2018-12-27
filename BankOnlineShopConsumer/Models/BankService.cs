using BankOnlineShopConsumer.BankReference;
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

        public int Transaction(Transaction transaction)
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
            var result = bank.AddTransaction(transactioNew);
            if (result == 1)
            {
                return 1;
            }
            else if (result == -1)
            {
                return -1;
            }
            else 
            {
                return 0;
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