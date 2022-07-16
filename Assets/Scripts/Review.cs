using System.Collections;
using UnityEngine;
using Google.Play.Review;
using Managers;

public class Review 
{
private static ReviewManager _reviewManager;
private static PlayReviewInfo _playReviewInfo;


    public static IEnumerator OpenReview()
    {
        _reviewManager = new ReviewManager();
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }

        _playReviewInfo = requestFlowOperation.GetResult();
        Debug.Log("playReviewInfo " + _playReviewInfo);

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }

        AnalyticManager.ReviewWasShow();
    }
}
