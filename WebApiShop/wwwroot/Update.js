
async function Update() {

    const email = document.querySelector("#userName").value;
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const password = document.querySelector("#password").value;
    q = await JSON.parse(sessionStorage.getItem("users"));

    const updateData = {
        Email: email,
        FirstName: firstName,
        LastName: lastName,
        Password: password,
    };

    const response = await fetch(`https://localhost:44378/api/users/${q.id}`,
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updateData)
        });

    if (!response.ok) {
        throw new Error(`HTTP error! status${response.status}`)
    }

    const data = await response.json();
    console.log('PUT Data:', data);
    if (response.ok) {
        alert("עודכן בהצלחה");
    }
    
}

