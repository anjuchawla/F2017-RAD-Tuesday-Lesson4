﻿/*
 * Name : Anju Chawla
 * Date: Oct 3, 2017
 * Purpose:This application allows the user to select multiple coffee type
 * in various quantities.
 * The item amount and total amount due is displayed.
 * */

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
    public partial class frmCoffeeShop : Form
    {
        //module level variables - have default values
        private decimal subTotalAmount, totalAmount, grandTotal;//0
        private RadioButton selectedRadioButton ; //null

        //module level constants
        const decimal TaxRate =  0.13m ;
        const decimal CappuccinoPrice = 2m;
        const decimal EspressoPrice = 2.25m;
        const decimal LattePrice = 1.75m;
        const decimal IcedPrice = 2.50m;




        public frmCoffeeShop()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This calculates the amount due for an order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //calculates the amount for a selected coffee and accumulates
            //the subtotal too since customer can order more than 
            //one coffee in different quantities

            //local variables-do not have a default value
            int quantity;
            decimal price, tax, itemAmount;

            //change settings
            chkTakeout.Enabled = false;
            btnNewOrder.Enabled = true;
            btnClear.Enabled = true;


            try
            {
                quantity = int.Parse(txtQuantity.Text);
                //quantity = Convert.ToInt32(txtQuantity.Text);
                if(quantity > 0)
                {
                    //is coffee selected
                    if(selectedRadioButton != null)
                    {
                        //please do not use individual if's-either use if/else or switch
                        switch(selectedRadioButton.Name)
                        {
                            case "rdoCappuccino":
                                price = CappuccinoPrice;
                                break;

                            case "rdoEspresso":
                                price = EspressoPrice;
                                break;

                            case "rdoLatte":
                                price = LattePrice;
                                break;
                            case "rdoIcedCappuccino":
                            case "rdoIcedLatte":
                                price = IcedPrice;
                                break;
                            default: price = 0;
                                break;

                        }//switch

                        //do the calculations
                        itemAmount = quantity * price;
                        subTotalAmount += itemAmount;
                        if (chkTakeout.Checked)
                            tax = TaxRate * subTotalAmount;
                        else
                            tax = 0;

                        totalAmount = subTotalAmount + tax;

                        //display results
                        txtItemAmount.Text = itemAmount.ToString("c");
                        txtSubtotal.Text = subTotalAmount.ToString("c");
                        txtTax.Text = tax.ToString("c");
                        txtTotalDue.Text = totalAmount.ToString("c");




                    }
                    else
                        {
                        MessageBox.Show("You must provide choose a coffee type.",
                     "Coffee Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }



                }
                else
                    {
                    MessageBox.Show("You must provide a positive number for the number of coffeess.",
                       "Quantity Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.SelectAll();
                    txtQuantity.Focus();

                }

            }
            catch (FormatException quantityFE)
            {
                if(txtQuantity.Text == String.Empty )
                {
                    MessageBox.Show("You must provide the number of coffees.",
                        "Quantity Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQuantity.Focus(); 

                }
                else
                {
                    MessageBox.Show("You must provide a positive number for the number of coffees.",
                       "Quantity Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.SelectAll();
                    txtQuantity.Focus();

                }
            }
            catch(OverflowException quantityOE)
            {
                MessageBox.Show("You must provide a positive number between 0 and " + int.MaxValue +" for the number of coffees.",
                       "Quantity Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuantity.SelectAll();
                txtQuantity.Focus();
            }
            catch(Exception quantityE)
            {
                MessageBox.Show(quantityE.Message ,
                      "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuantity.SelectAll();
                txtQuantity.Focus();
            }



           

                      


        }

        private void frmCoffeeShop_Load(object sender, EventArgs e)
        {
            //initial seetings
            btnClear.Enabled = false;
            btnNewOrder.Enabled = false;

        }
        /// <summary>
        /// Sets the coffee selected by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            selectedRadioButton = (RadioButton)sender;
          
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clear the user input and get back to start default state
            ClearInput();

        }

        private void ClearInput()
        {
            txtQuantity.Clear();
            txtItemAmount.Clear();
            if(selectedRadioButton != null)
            {
                selectedRadioButton.Checked = false;
                selectedRadioButton = null;
            }
            txtQuantity.Focus();
          
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            frmAboutBox aboutUs = new frmAboutBox();
            aboutUs.ShowDialog();
        }

        /// <summary>
        /// 
        /// A new order is placed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            DialogResult confirm;

            confirm = MessageBox.Show("Are you sure you want to place a new order?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

            if(confirm == DialogResult.Yes)

            {
                ClearInput();
                btnClear.Enabled = false;
                btnNewOrder.Enabled = false;
                txtItemAmount.Clear();
                txtSubtotal.Clear();
                txtTax.Clear();
                txtTotalDue.Clear();
                chkTakeout.Enabled = true;
                chkTakeout.Checked = false;
               

                //reset total
                subTotalAmount = 0;
                totalAmount = 0;


            }
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //close the form
            this.Close();
           
        }
    }
}
