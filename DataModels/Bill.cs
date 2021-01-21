using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenHtmlToPdf;

namespace RecieptGenerator.DataModels
{
    class Bill
    {
        public Dictionary<int, Item> itemList = new Dictionary<int, Item>();
        public string CustomerName;
        public string CustomerMobile;
        public DateTime BillDate;
        public int BillAmount;
        public int TotalItems;
        public int totalItemTypes = 0;
        public string basePath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        public string billBodyTemplate = "<tr class=\"itemboxes\"><td>@SrNo</td><td>@Item</td><td>@Price</td><td>@Quantity</td><td>@Amount</td></tr>";
        
        public void startBinding(string cName, string cMobile)
        {
            this.itemList.Clear();
            this.CustomerName = cName;
            this.CustomerMobile = cMobile;
            this.BillDate = DateTime.Now;
            this.BillAmount = 0;
            this.TotalItems = 0;
        }
        public void generatePDF()
        {
            var billBody = "";
            foreach(KeyValuePair<int,Item> keyValuePair in this.itemList)
            {
                var itemBody = "";
                this.BillAmount += keyValuePair.Value.amount;
                this.TotalItems += keyValuePair.Value.quantity;
                itemBody = this.billBodyTemplate.Replace("@SrNo", keyValuePair.Key.ToString());
                itemBody = itemBody.Replace("@Item", keyValuePair.Value.name.ToString());
                itemBody = itemBody.Replace("@Price", keyValuePair.Value.price.ToString());
                itemBody = itemBody.Replace("@Quantity", keyValuePair.Value.quantity.ToString());
                itemBody = itemBody.Replace("@Amount", keyValuePair.Value.amount.ToString());
                billBody += itemBody;
            }

            var temp = basePath.ToString() + "/Templates/template.html";
            var pdfs = basePath.ToString() + "/Pdfs/" + this.CustomerName.ToString()+ this.BillDate.ToString("ddMMMyyyy") + ".pdf";
            string html = System.IO.File.ReadAllText(temp);
            html = html.Replace("@CustomerName", this.CustomerName.ToString());
            html = html.Replace("@CustomerMobile", this.CustomerMobile.ToString());
            html = html.Replace("@Date", this.BillDate.ToString("dd-MMM-yyyy"));
            html = html.Replace("@Amount", this.BillAmount.ToString());
            html = html.Replace("@TotalItems", this.TotalItems.ToString());
            html = html.Replace("@BillBody", billBody.ToString());
            var pdf = Pdf.From(html);
            byte[] content = pdf.Content();
            File.WriteAllBytes(pdfs, content);
        }
        public void addItem(string iname,int iprice, int iquantity)
        {
            totalItemTypes++;
            itemList.Add(totalItemTypes, new Item(iname, iprice, iquantity));
        }
    }
}
