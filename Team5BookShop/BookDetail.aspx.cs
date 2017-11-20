﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BookshopEntities1 bk = new BookshopEntities1();
    int bookId;
    Book bookItem;
    protected void Page_Load(object sender, EventArgs e)
    {
        string val = Request.QueryString["BookID"];
        if (val != null)
        {
            bookId = Convert.ToInt32(val);
            bookItem = new BookBusinesslogic().GetBookDetails(bookId);
            lblTitle.Text = bookItem.Title;
            lblCat.Text = Convert.ToString(bookItem.Category);
            lblISBN.Text = bookItem.ISBN;
            lblAuthor.Text = bookItem.Author;
            lblPrice.Text = Convert.ToString(bookItem.Price);
        }
    }
}

 