function testme() {
    console.log("In js file!");
}

function dotNetStaticInvocation() {
    DotNet.invokeMethodAsync("BlazorMovies.Client", "GetCurrentCount")
        .then(result => console.log("from method?: " + result));
}

function dotnetInstanceInvocation(dotnetHelper) {
    console.log("In instance-js!");
    dotnetHelper.invokeMethodAsync("IncrementCount");
}