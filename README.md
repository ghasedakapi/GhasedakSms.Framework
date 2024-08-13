# GhasedakSms.Framework

**GhasedakSms.Framework** is a .NET Framework library designed to interact with the [Ghasedak SMS API](https://ghasedak.me/), allowing you to send SMS, check statuses, and manage SMS-related functionalities efficiently.

## Features

- **Send SMS:** Supports sending single, bulk, and pair-to-pair SMS.
- **OTP Management:** Easily send OTP messages with customizable parameters.
- **Receive SMS:** Retrieve received messages with pagination support.
- **Account Information:** Fetch account details like balance and other information.

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package GhasedakSms.Framework
```

Or using the NuGet Package Manager Console:

```bash
Install-Package GhasedakSms.Framework
```

## Usage

### 1. Configuration

To start using the library, initialize the `Ghasedak` client with your API key:

```csharp
var apiKey = "your_api_key";
var client = new Ghasedak(apiKey);
```

### 2. Fetch Account Information

Retrieve account information such as balance:

```csharp
var accountInfo = await client.GetAccountInformation();
```

### 3. Receiving SMS

#### Retrieve Received SMS:

```csharp
var receivedMessages = await client.GetReceivedSmses(new GetReceivedSmsInput
{
    LineNumber = "30005088",
    IsRead = true
});
```

#### Retrieve Received SMS with Pagination:

```csharp
var receivedMessagesPaged = await client.GetReceivedSmsesPaging(new GetReceivedSmsPagingInput
{
    LineNumber = "30005088",
    IsRead = true,
    StartDate = DateTime.Now.AddDays(-7),
    EndDate = DateTime.Now,
    PageIndex = 1,
    PageSize = 10
});
```

### 4. OTP Operations

#### Fetch OTP Template Parameters:

```csharp
var otpTemplate = await client.GetOtpParameters(new GetOtpParametersInput
{
    TemplateName = "Ghasedak"
});
```

#### Send OTP SMS:

```csharp
var sendOtp = await client.SendOtpSMS(new SendOtpInput
{
    TemplateName = "Ghasedak",
    SendDate = DateTime.Now,
    Inputs = new List<OtpInput>
    {
        new OtpInput { Param = "Code", Value = "1234" },
    },
    Receptors = new List<SendOtpReceptorDto>
    {
        new SendOtpReceptorDto { ClientReferenceId = "testOtp1", Mobile = "0912*******" },
        new SendOtpReceptorDto { ClientReferenceId = "testOtp2", Mobile = "0912*******" }
    }
});
```

#### Send OTP with Predetermined Parameters

The `SendOtpWithParams` function allows you to send an OTP message using predetermined parameters.

```csharp
var sendOtpWithParams = await client.SendOtpWithParams(new SendOtpWithParamsInput
{
    TemplateName = "otpTemplate",
    Param1 = "value1",
    Param2 = "value2",
    Param3 = "value3",
    Param4 = "value4",
    Param5 = "value5",
    Param6 = "value6",
    Param7 = "value7",
    Param8 = "value8",
    Param9 = "value9",
    Param10 = "value10",
    Receptors = new List<SendOtpReceptorDto>
    {
        new SendOtpReceptorDto { ClientReferenceId = "otpTest1", Mobile = "0912*******" },
        new SendOtpReceptorDto { ClientReferenceId = "otpTest1", Mobile = "0912*******" },
    },
    SendDate = DateTime.Now
});
```

### 5. Sending SMS

#### Send a Single SMS:

```csharp
var sendSingle = await client.SendSingleSMS(new SendSingleSmsInput
{
    LineNumber = "30005088",
    ClientReferenceId = "testsingle",
    Message = "test single",
    Receptor = "0912*******",
    SendDate = DateTime.Now
});
```

#### Send Bulk SMS:

```csharp
var sendBulk = await client.SendBulkSMS(new SendBulkInput
{
    LineNumber = "30005088",
    Message = "test bulk",
    Receptors = new List<string> { "0912*******", "0912*******" },
    ClientReferenceId = "testbulk",
    SendDate = DateTime.Now
});
```

#### Send Pair-to-Pair SMS:

```csharp
var sendPair = await client.SendPairToPairSMS(new SendPairToPairInput
{
    Items = new List<SendPairToPairSmsWebServiceDto>
    {
        new SendPairToPairSmsWebServiceDto
        {
            ClientReferenceId = "testpair",
            LineNumber = "30005088",
            Message = "test pair to pair",
            Receptor = "0912*******",
            SendDate = DateTime.Now
        }
    }
});
```

### 6. Checking SMS Status

```csharp
var statuses = await client.CheckSmsStatus(new CheckSmsStatusInput
{
    Type = MessageIdType.MessageId,
    Ids = new List<string> { "12345", "67890" } // Replace with actual Message IDs
});
```

## Contributing

Contributions are welcome! Feel free to fork the repository, open issues, or submit pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or support, please reach out to the Ghasedak team at [support@ghasedak-ict.com](mailto:support@ghasedak-ict.com).
