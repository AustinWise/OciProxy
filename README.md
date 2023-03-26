
## TODO

* Make upstream configurable.
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
