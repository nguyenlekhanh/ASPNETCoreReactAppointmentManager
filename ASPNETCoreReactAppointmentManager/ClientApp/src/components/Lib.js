export const testData = [
  { ID: 1, Title: "Title 1", Description: "Description 1", LevelOfImportance: 3, Date: "14-04-2023", Time: "12:00", Address: "address 1"},
  { ID: 2, Title: "Title 2", Description: "Description 2", LevelOfImportance: 4, Date: "13-04-2023", Time: "1:00", Address: "address 2"},
  { ID: 3, Title: "Title 3", Description: "Description 3", LevelOfImportance: 5, Date: "12-04-2023", Time: "2:00", Address: "address 3"},
  { ID: 4, Title: "Title 4", Description: "Description 4", LevelOfImportance: 0, Date: "10-04-2023", Time: "3:00", Address: "address 4"}
];

const url = "api/appointment"

export const entry = {

}

export async function getDefault() {
  const res = await fetch(url);

  if(!res.ok && !res.status !== 200) {
    console.log("Error:", res);
    notifyUser("Something went wrong, please refresh the page.")
    return [];
  }

  return await res.json();
}

export async function postAppointment(newApp) {
  const res = await fetch(url, {
    method: "POST",
    body: JSON.stringify(newApp),
    headers: {
      "content-type": "application/json"
    }
  });

  if(!res.ok) {
    console.log("Error:", res);
    notifyUser("Something went wrong, please refresh the page.")
    return [];
  }

  return await res.json();
}

export function notifyUser(msg) {
  const notificationEl = document.querySelector(".notification");
  notificationEl.innerHTML = msg ? msg : ""

  if(msg) {
    setTimeout(() => {
      notificationEl.innerHTML = ""
    }, 12000);
  }
}