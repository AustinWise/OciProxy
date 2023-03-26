
# An OCI Registry Proxy

This point of this tool is to allow you to easily and cheaply define you own custom domain name for your OCI
images.
(These are also known a Docker images).
By doing so, where your OCI images physically live is no longer tied to name of your image.
This allows you to move the location where your image is hosted without breaking everyone who uses your image.

A longer term goal of this project is enable you to easily mirror your container registry to different clouds.
This will give consumers of your images a faster download experience while also saving you money on cloud egress
charges.

## TODO

* Figure out a versioning strategy for our OCI image. Currently the build script always pushes version `1.0.0`.
* Put something more interesting on the index page.

Not for MVP, but eventually this would be nice:

* Add support for redirecting to object storage based on source IP.
* Add a program that copies blobs to object storage for the above feature. This could run:
  * On some sort of schedule.
  * Automatically based on Pub-Sub notifications when a new image is pushed.
* Maybe add some sort of analytics that can automatically mirror images to new cloud regions based on access patterns.

## Developer Docs

* OCI Distribution Spec: https://github.com/opencontainers/distribution-spec/blob/main/spec.md
* .NET Minimal APIs docs: https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/overview
* Publishing this program as a container with: https://learn.microsoft.com/dotnet/core/docker/publish-as-container
* Inspiration: https://github.com/kubernetes/registry.k8s.io/

Test Command:

```
docker pull images.happy-turtle.dev/oci-proxy:1.0.0
```
