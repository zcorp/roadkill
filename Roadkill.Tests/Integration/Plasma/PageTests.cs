﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Roadkill.Core;
using Roadkill.Tests.Core;
using NUnit.Framework;
using System.IO;

using Plasma.Core;
using Plasma.WebDriver;
using OpenQA.Selenium;
using System.Diagnostics;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace Roadkill.Tests.Integration.Plasma
{
	/// <summary>
	/// Performs basic tests for correct HTML from the site.
	/// </summary>
	[TestFixture]
	[Explicit]
	[Category("Integration")]
	public class PageTests : PlasmaTestBase
	{
		[Test]
		public void Homepage_HasContent()
		{
			AspNetResponse homePage = AppInstance.ProcessRequest("/");

			HtmlDocument html = new HtmlDocument();
			html.LoadHtml(homePage.BodyAsString);

			HtmlNode document = html.DocumentNode;
			IEnumerable<HtmlNode> h1Elements = document.QuerySelectorAll("h1");
			Assert.That(h1Elements.Count(), Is.GreaterThan(0));

			IEnumerable<HtmlNode> navigation = document.QuerySelectorAll("#leftmenu>ul>li");
			Assert.That(navigation.Count(), Is.EqualTo(3));

			HtmlNode loggedIn = document.QuerySelector("#loggedinas");
			Assert.That(loggedIn.InnerText, Is.EqualTo("Not logged in&nbsp;-&nbsp;Login"));
		}

		[Test]
		public void Login_HasContent()
		{
			AspNetResponse loginPage = AppInstance.ProcessRequest("/user/login");

			HtmlDocument html = new HtmlDocument();
			html.LoadHtml(loginPage.BodyAsString);

			var document = html.DocumentNode;
			IEnumerable<HtmlNode> loginH1 = document.QuerySelectorAll("h1");
			Assert.That(loginH1.First().InnerText, Is.EqualTo("Login"));

			HtmlNode emailTextBox = document.QuerySelector("fieldset input#email");
			Assert.NotNull(emailTextBox);

			HtmlNode passwordTextBox = document.QuerySelector("fieldset input#password");
			Assert.NotNull(passwordTextBox);

			HtmlNode loginButton = document.QuerySelector("#userbutton");
			Assert.That(loginButton.Attributes["value"].Value, Is.EqualTo("Login"));
		}

		[Test]
		public void Login_AsAdmin()
		{
			AspNetRequest request = new AspNetRequest("/user/login");
			request.Method = "POST";
			request.QueryString = "email=admin@localhost&password=password";

			AspNetResponse loginPage = AppInstance.ProcessRequest(request);
			Assert.That(loginPage.Status, Is.EqualTo(302), loginPage.BodyAsString);

			string followUrl = ParseFollowUrl(loginPage.BodyAsString);

			// Redirected to the homepage from the login page, add the cookies we got
			request = new AspNetRequest(followUrl);
			request.AddCookies(loginPage.Cookies);
			AspNetResponse homePage = AppInstance.ProcessRequest(request);
			HtmlDocument html = new HtmlDocument();
			html.LoadHtml(homePage.BodyAsString);

			HtmlNode document = html.DocumentNode;
			HtmlNode loggedIn = document.QuerySelector("#loggedinas");
			Assert.That(loggedIn.InnerText, Is.EqualTo("Logged in as admin&nbsp;-&nbsp;Logout"));
		}

		[Test]
		public void Test_HasContent()
		{
			AspNetResponse homePage = AppInstance.ProcessRequest("/");

			HtmlDocument html = new HtmlDocument();
			html.LoadHtml(homePage.BodyAsString);

			HtmlNode document = html.DocumentNode;
			IEnumerable<HtmlNode> h1Elements = document.QuerySelectorAll("h1");
			Assert.That(h1Elements.Count(), Is.GreaterThan(0));

			IEnumerable<HtmlNode> navigation = document.QuerySelectorAll("#leftmenu>ul>li");
			Assert.That(navigation.Count(), Is.EqualTo(3));

			HtmlNode loggedIn = document.QuerySelector("#loggedinas");
			Assert.That(loggedIn.InnerText, Is.EqualTo("Not logged in&nbsp;-&nbsp;Login"));
		}
	}
}