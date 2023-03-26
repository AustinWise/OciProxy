
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

* Make upstream configurable.
* Figure out a versioning strategy for our OCI image. Currently the build script always pushes version `1.0.0`.
* Check if we really need to use `MapMethods` with `GET` and `HEAD`. I think `MapGet` might work.

Not for MVP, but eventually this would be nice:

* Add support for redirecting to object storage based on source IP.
* Maybe blindly trust the `X-Forwarded-For` header, since it is not a security mechanism. It is just a bandwidth optimization.

## Developer Docs

https://github.com/opencontainers/distribution-spec/blob/main/spec.md
https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/overview

Test Command:

```
docker pull oci-proxy-eafg4b64zq-uc.a.run.app/oci-proxy:1.0.0
```
