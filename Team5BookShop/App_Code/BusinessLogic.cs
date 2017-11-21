﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

/// <summary>
/// Summary description for BusinessLogic
/// </summary>
public class BusinessLogic
{
    //01 homepage methods
    public List<Book> GetAllBooks()
    {
        using (BookshopEntities context = new BookshopEntities())
        {
            return context.Books.ToList<Book>();
        }
    }

    public List<Book> GetBooksByTitle(string title)
    {
        using (BookshopEntities context = new BookshopEntities())
        {
            return context.Books.Where(book => book.Title.Contains(title)).ToList<Book>();
        }
    }

    public List<Book> GetBooksByCategoryID(int categoryID)
    {
        using (BookshopEntities context = new BookshopEntities())
        {
            return context.Books.Where(book => book.CategoryID == categoryID).ToList<Book>();
        }
    }

    //02 login page methods (by inetmgr)

    //03 registration page methods (by inetmgr)

    //04 shopping cart page methods



    //public void Checkout(string userID, string mailingAddress, DateTime orderDate, decimal totalPrice, ShoppingCart shoppoingCart)
    //{
    //    using (BookshopEntities context = new BookshopEntities())
    //    {
    //        //Create Order
    //        Order order = new Order();
    //        order.UserID = userID;
    //        order.MailingAddress = mailingAddress;
    //        order.OrderDate = orderDate;
    //        order.TotalPrice = totalPrice;
    //        context.Orders.Add(order);
    //        context.SaveChanges();

    //        //Create OrderDetails from CartItems
    //        foreach (CartItem item in shoppingCart)
    //        {
    //            OrderDetail orderDetail = new OrderDetail();
    //            orderDetail.OrderID = context.Orders.Max(x => x.OrderID);
    //            orderDetail.BookID = item.BookID;
    //            orderDetail.Quantity = item.Quantity;
    //            orderDetail.UnitPrice = item.UnitPrice;
    //            orderDetail.SubTotalPrice = item.SubTotal;
    //            context.OrderDetails.Add(orderDetail);
    //        }
    //        context.SaveChanges();

    //        //Clear ShoppingCart
    //        shoppingCart.Clear();
    //    }


    //}

    public bool Checkout(string userID, string mailingAddress, DateTime orderDate, decimal totalPrice, ShoppingCart shoppingCart)
    {
        using (BookshopEntities context = new BookshopEntities())
        {
            //Create Order
            Order order = new Order();
            int userIDHolder;
            bool validUserID = Int32.TryParse(userID, out userIDHolder);

            order.MailingAddress = mailingAddress;
            order.OrderDate = orderDate;
            order.TotalPrice = Convert.ToDouble(totalPrice);

            if (validUserID)
            {
                try
                {
                    order.UserID = userIDHolder;
                    context.Orders.Add(order);
                    context.SaveChanges();

                    //Create OrderDetails from CartItems
                    foreach (CartItem item in shoppingCart.Cart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.BookID = Convert.ToInt16(item.BookID);
                        orderDetail.Quantity = Convert.ToInt16(item.Quantity);
                        orderDetail.UnitPrice = Convert.ToDouble(item.UnitPrice);
                        orderDetail.SubtotalPrice = Convert.ToDouble(item.SubTotal);
                        context.OrderDetails.Add(orderDetail);
                    }
                    context.SaveChanges();

                    //Clear ShoppingCart
                    shoppingCart.Clear();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    public List<string> GetCategoryList()
    {
        using (BookshopEntities context = new BookshopEntities())
        {
            List<string> catList = context.Categories.Select(x => x.CategoryName).ToList();
            return catList;
        }
    }

    public void imageAssign(List<Book> lstBook, PlaceHolder ph)
    {
        Table t = new Table();

        for (int i = 0; i < lstBook.Count; i++)
        {
            //dynamic table row
            TableRow tr1 = new TableRow();

            //column for image
            TableCell tc1 = new TableCell();
            tc1.Width = 200;

            ImageButton img = new ImageButton();
            img.Width = 200;
            img.Height = 200;
            string imgName = lstBook[i].ISBN;
            img.ImageUrl = "images/" + imgName + ".jpg";

            tc1.Controls.Add(img);
            tr1.Cells.Add(tc1);
            img.PostBackUrl = "~/bookDetail.aspx?BookID=" + imgName;


            //column for title
            TableCell tc2 = new TableCell();
            tc2.Width = 400;

            Label lblTitle = new Label();
            lblTitle.Text = lstBook[i].Title.ToString();

            tc2.Controls.Add(lblTitle);
            tr1.Cells.Add(tc2);

            //column for price
            TableCell tc3 = new TableCell();
            tc3.Width = 200;

            Label lblPrice = new Label();            
            lblPrice.Text = lstBook[i].Price.ToString();

            tc3.Controls.Add(lblPrice);
            tr1.Cells.Add(tc3);

            //column for edit book button   
            TableCell tc4 = new TableCell();
            tc4.Width = 200;

            Button btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.PostBackUrl = "~/BookUpdate.aspx?ISBN=" + lstBook[i].ISBN;

            tc4.Controls.Add(btnEdit);
            tr1.Cells.Add(tc4);

            //add to control panel
            t.Rows.Add(tr1);
            ph.Controls.Add(t);
        }       
    }
    public void imageAssignForCustomer(List<Book> lstBook, PlaceHolder ph)
    {  
        Table t = new Table();

        for (int i = 0; i < lstBook.Count; i++)
        {
            //dynamic table row
            TableRow tr1 = new TableRow();

            //column for image
            TableCell tc1 = new TableCell();
            tc1.Width = 200;
            
            ImageButton img = new ImageButton();
            img.Width = 200;
            img.Height = 200;
            string imgName = lstBook[i].ISBN;
            img.ImageUrl = "images/" + imgName + ".jpg";

            tc1.Controls.Add(img);
            tr1.Cells.Add(tc1);
            img.PostBackUrl = "~/bookDetail.aspx?BookID=" + imgName;

            
            //column for title
            TableCell tc2 = new TableCell();
            tc2.Width = 400;

            Label lblTitle = new Label();
            lblTitle.Text = lstBook[i].Title.ToString();

            tc2.Controls.Add(lblTitle);
            tr1.Cells.Add(tc2);

            //column for price
            TableCell tc3 = new TableCell();
            tc3.Width = 200;

            Label lblPrice = new Label();
            lblPrice.Text = lstBook[i].Price.ToString();

            tc3.Controls.Add(lblPrice);
            tr1.Cells.Add(tc3);
                    
            //column for view detail button    
            TableCell tc4 = new TableCell();
            tc4.Width = 200;

            Button btnViewDetail = new Button();
            btnViewDetail.Text = "View Detail";
            btnViewDetail.PostBackUrl = "~/bookDetail.aspx?BookID=" + imgName;

            tc4.Controls.Add(btnViewDetail);
            tr1.Cells.Add(tc4);          

            //add to control panel
            t.Rows.Add(tr1);
            ph.Controls.Add(t);
        }
    }

}