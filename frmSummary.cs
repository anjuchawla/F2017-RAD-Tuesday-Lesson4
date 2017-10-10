using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coffee_Sales
{
    public partial class frmSummary : Form
    {
        
        public frmSummary()
        {
            InitializeComponent();

            //populate the summary information
            txtCustomerCount.Text = frmCoffeeShop.customerCount.ToString() ;
            txtGrandTotalAmount.Text = frmCoffeeShop.grandTotalAmount.ToString("c");
            txtAverage.Text = frmCoffeeShop.average.ToString("c");

        }

       

        private void btnOK_Click(object sender, EventArgs e)
        {
            //close the form
            this.Close();
        }
    }
}
