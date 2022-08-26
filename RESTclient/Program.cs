// See https://aka.ms/new-console-template for more information

using RESTclient;

ConsumeWebApi consumeWebApi = new ConsumeWebApi();
await consumeWebApi.PostCustomersInParallel();
