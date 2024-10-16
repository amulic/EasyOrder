using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Amazon.Runtime;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.Mime.MediaTypeNames;

public class BackblazePresignedUrlGenerator
{
    private readonly string bucketName = "EasyOrder";
    private readonly string accessKey = "your_access_key";
    private readonly string secretKey = "your_secret_key";
    private readonly string serviceUrl = "your_service_url"; // Backblaze S3-compatible endpoint

    private readonly IAmazonS3 s3Client;

    public BackblazePresignedUrlGenerator()
    {
        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        var config = new AmazonS3Config
        {
            ServiceURL = serviceUrl, // Set the Backblaze service URL
            ForcePathStyle = true // This ensures compatibility with Backblaze's path-style URLs
        };
        s3Client = new AmazonS3Client(credentials, config);
    }

    public string GeneratePresignedUrl(string objectKey, string contentType, TimeSpan expiration)
    {
        // Ensure the bucket exists
        if (!AmazonS3Util.DoesS3BucketExistV2Async(s3Client, bucketName).Result)
        {
            throw new Exception($"Bucket {bucketName} does not exist");
        }

        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = objectKey, // The file path in the bucket
            Verb = HttpVerb.PUT, // Use PUT for uploads
            Expires = DateTime.UtcNow.Add(expiration), // Set the expiration for the URL
            ContentType = contentType, // The expected content type of the file
        };

        // Generate the pre-signed URL
        string url = s3Client.GetPreSignedURL(request);
        return url;
    }
}
