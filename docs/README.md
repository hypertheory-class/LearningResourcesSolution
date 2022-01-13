# Learning Resources Api

This API supplies content for a proposed Angular application that allows users to create lists of helpful learning resources on various (programming) topics.

It will allow them to retrieve a list of resources.

It will allow them to submit new resources.

It will allow them to mark resources as "viewed".

It will allow them to add notes to viewed resources.

## Authentication/Authorization

We will implement this in a later sprint. (uh, next week?)

## Resources

### LearningResource

GET /learningresources/1

```json
{
    "id": 1,
    "title": "Using the Command Line",
    "description": "Tips on using the command line in powershell",
    "link": "https://youtube.com/some-video"
}
```

GET /learningresources

```json
{
    "data" : [
        {
            "id": 1,
            "title": "Using the Command Line"
        },
        {
            "id": 2,
            "title": "Git for Dummies"
        }
    ]
}
```