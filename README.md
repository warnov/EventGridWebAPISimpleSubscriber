# Azure Event Grid WebAPI Simple Subscriber
This is a WebAPI Controller designed to be a subscriber for Azure Event Grid.

It contains a POST handler that evaluates if the request is for Event Grid Validation, or if it is an operational event.
In the first case it returns the validation code. Otherwise, the last event is saved in the server memory, just to be requested using the get method.
