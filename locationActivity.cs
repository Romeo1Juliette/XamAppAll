﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyXamarinApp.Activities;
using MyXamarinApp.Classes;
using MyXamarinApp.Classes.Repositories;

namespace MyXamarinApp
{
    [Activity(Label = "@string/app_name",Theme = "@style/AppTheme")]
    public class locationActivity : Activity
    {
        Button deleteBtn, clickInfo, counDisplayertBtn;
        ImageButton backBtn;
        LinearLayout linLyt;
        int counter;
        ListView listLocation;
        string msg;
        List<string> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {   
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.locationLayout);

            deleteBtn = FindViewById<Button>(Resource.Id.deleteBtn);

            backBtn = FindViewById<ImageButton>(Resource.Id.backBtn);

            counDisplayertBtn = FindViewById<Button>(Resource.Id.counDisplayertBtn);

            linLyt = FindViewById<LinearLayout>(Resource.Id.linLyt);

            listLocation = FindViewById<ListView>(Resource.Id.listLocation);

           
            
            listLocation.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, LocationsRepository.DisplayTableContent() );

            msg = Resources.GetText(Resource.String.clickInfo);
            if (savedInstanceState != null) { counter = savedInstanceState.GetInt("counter",0); }
            else
            {
                counter = 0;
            }
            counDisplayertBtn.Text= $"{counter++} { msg}";
            clickInfo = new Button(this);
            backBtn.Click += delegate
            {
                Intent intent = new Intent(this,typeof(LocationActivity));

                StartActivity(intent);
            };
            counDisplayertBtn.Click += delegate
            {
                
                counDisplayertBtn.Text = $"{counter++} { msg}";
                
            };

            deleteBtn.Click += (s, e) =>
            {
                LocationsRepository.DeleteContent();
                listLocation.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Array.Empty<string>());
            };
           
            
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (outState != null)
            {
                outState.PutInt("counter", counter);
            }

            base.OnSaveInstanceState(outState);
        }
    }
}