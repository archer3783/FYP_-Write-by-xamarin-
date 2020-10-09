﻿using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {

        public string WebAPIkey = "AIzaSyAIFwIiakmB2aCvW6BEKhPheokVAYTgjGc";

        public SignupPage()
        {
            InitializeComponent();
        }

        async private void btn_Signup_Clicked(object sender, EventArgs e)
        {
            if (ent_Email == null || ent_comPass == null || ent_Password == null)
            {
                await DisplayAlert("Alert", "Please input all detail", "OK");
            } else if (ent_Password.Text != ent_comPass.Text)
            {
                await DisplayAlert("Alert", "Password and Comfirm Password must be same", "OK");
            } else
            {
                try
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(ent_Email.Text, ent_Password.Text);
                    string gettoken = auth.FirebaseToken;
                    //await App.Current.MainPage.DisplayAlert("Alert", gettoken, "Ok");
                    await DisplayAlert("Alert", "Sign Up finish", "OK");
                    await Navigation.PopModalAsync();
                }
                catch (Exception ex)
                {
                    //await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                    await DisplayAlert("Alert", "Sign Up fail", "OK");
                }

                
            }

        }
    }
}