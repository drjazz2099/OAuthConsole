﻿using System;
using System.Net;

namespace OAuthSig
{
    internal class OAuthRequest
    {
        private readonly IView _view;

        public OAuthRequest(IView view)
        {
            _view = view;
        }

        public void Request()
        {
            string httpMethod = _view.HttpMethod;
            if (String.IsNullOrEmpty(_view.GeneratedUrl))
            {
                return;
            }

            if (httpMethod == "POST")
            {
                ApiPostRequestBuilder apiPostRequestBuilder = new ApiPostRequestBuilder();
                Uri uri = new Uri(_view.Uri);
                string response = apiPostRequestBuilder.Build(true,
                                                              uri, _view.PostData,
                                                              _view.ConsumerKey,
                                                              _view.ConsumerSecret, _view.Token,
                                                              _view.TokenSecret, _view.RawSignature, _view.Nonce, _view.TimeStamp);
                _view.DisplayResponse(response);
            }
            else
            {
                WebClient webClient = new WebClient();
                string response = webClient.DownloadString(_view.GeneratedUrl);
                _view.DisplayResponse(response);
            }

            _view.Log();

        }
    }
}