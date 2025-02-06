export const login = (username, password) => {
  if (username === "user" && password === "user") { 
    localStorage.setItem("user", JSON.stringify({ username }));
    return true;
  }
  return false;
};

export const logout = () => {
  localStorage.removeItem("user");
};

export const getUser = () => {
  return JSON.parse(localStorage.getItem("user"));
};
