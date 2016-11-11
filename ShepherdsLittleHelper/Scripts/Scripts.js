function userAdded(email) {
    if (email)
    {
        document.getElementById("userAddedMessage").innerHTML = "User " + email + " added to group!";
    }
    else
    {
        document.getElementById("userAddedMessage").innerHTML = "Could not find user with that email.";
    }
}