# Sitecore Send Postman Collection

This Postman collection is based on previously generated (see [article](https://neilkillen.com/2021/12/16/creating-a-postman-collection-for-sitecore-send-moosend/) from Neil Killen for more details).

Features:
- `apiKey` should be configured on Environment level and is automatically used in all requests
- internal variables, e.g. `CampaignID` or `mailingListId` - you can retrieve it once by id and then use in other get statistics / update requests

It contains `Internal API` and `Public API` sections.
