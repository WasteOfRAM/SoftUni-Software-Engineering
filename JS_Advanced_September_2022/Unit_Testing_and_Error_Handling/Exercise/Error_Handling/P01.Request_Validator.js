function requestValidator(obj) {
    let validMethods = ["GET", "POST", "DELETE", "CONNECT"];
    let validVersions = ["HTTP/0.9", "HTTP/1.0", "HTTP/1.1", "HTTP/2.0"];
    let urlPatern = /^[\w.]+$/g;
    let messagePatern = /[<>\\&'"]/g;

    if (!obj.hasOwnProperty('method')) {
        throw new Error("Invalid request header: Invalid Method");
    }

    if (!validMethods.includes(obj.method)) {
        throw new Error("Invalid request header: Invalid Method");
    }

    if (!obj.hasOwnProperty('uri')) {
        throw new Error("Invalid request header: Invalid URI");
    }

    if (obj.uri !== "*" && !urlPatern.test(obj.uri)) {
        throw new Error("Invalid request header: Invalid URI");
    }

    if (!obj.hasOwnProperty('version')) {
        throw new Error("Invalid request header: Invalid Version");
    }

    if (!validVersions.includes(obj.version)) {
        throw new Error("Invalid request header: Invalid Version");
    }

    if (!obj.hasOwnProperty('message')) {
        throw new Error("Invalid request header: Invalid Message");
    }

    if (obj.message !== '' && messagePatern.test(obj.message)) {
        throw new Error("Invalid request header: Invalid Message");
    }

    return obj;
}

let result = requestValidator({
    method: 'GET',
    uri: 'svn.public.catalog',
    version: 'HTTP/1.1',
    message: ''
});

console.log(result);

requestValidator({
    method: 'OPTIONS',
    uri: 'git.master',
    version: 'HTTP/1.1',
    message: '-recursive'
});

requestValidator({
    method: 'POST',
    uri: 'home.bash',
    message: 'rm -rf /*'
});

