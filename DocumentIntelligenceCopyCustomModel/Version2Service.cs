using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentIntelligenceCopyCustomModel
{
    public class Version2Service
    {
        private FormTrainingClient _destformRecognizerClient;
        private FormTrainingClient srcformRecognizerClient;

        public Version2Service()
        {
_destformRecognizerClient = new FormTrainingClient(new Uri("https://myformrecognizer.cognitiveservices.azure.com/"), new AzureKeyCredential("your-key"));
            srcformRecognizerClient = new FormTrainingClient(new Uri("https://myformrecognizer.cognitiveservices.azure.com/"), new AzureKeyCredential("your-key"));
        }

        public async Task  CopyV2CustomModel()
        {
            var srcmodelId = "your-guid-here";
            //this acquired authorization for copy along with placeholder Guid for model in destination region
            var authResponse = await _destformRecognizerClient.GetCopyAuthorizationAsync("/subscriptions/your-subscription-id/resourceGroups/your-resource-group/providers/Microsoft.CognitiveServices/accounts/your-cognitive-service-name", "region").WaitAsync(new CancellationToken());// call
           // authResponse.Value.ModelId; This is will be the Guid of the model in destination region

            var result = await srcformRecognizerClient.StartCopyModelAsync(srcmodelId,authResponse).WaitAsync(new CancellationToken());

            //result.Value.ModelId This is the modelid of newly copied model. same as authResponse.Value.ModelId
        }
    }
}
