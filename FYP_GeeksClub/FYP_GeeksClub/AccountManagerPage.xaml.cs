﻿using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountManagerPage : ContentPage
    {


        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        MediaFile file;


        public AccountManagerPage()
        {
            InitializeComponent();

            GetUserAccountDetails();
        }

        protected override bool OnBackButtonPressed()
        {
            var tabbedPage = this.Parent as TabbedPage;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    tabbedPage.CurrentPage = new HomePage();
                }
            });
            return true;
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

        }

         private void Save_Clicked(object sender, EventArgs e)
        {
            firebaseHelper.UpdateUserName(UserName.Text.ToString());
            GetUserAccountDetails();   
        }


        public async void GetUserAccountDetails()
        {
            await Task.Delay(2000);
            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            if (GetAccount != null)
            {
                lb_user.Text = GetAccount.Object.UserName.ToString();
            }
            else
            {
                lb_user.Text = "null";
            }
        }

        async private void Upload_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UploadImage(file.GetStream(), Preferences.Get("email", "").ToString());
        }

        async private void Pick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async private void Download_Clicked(object sender, EventArgs e)
        {
            var Getfile = await firebaseHelper.GetFile(Preferences.Get("email", "").ToString());
            
            imgChoosed.Source = Getfile.ToString();
        }
        
    }

       


    }