const testUser = {
    username: "user",
    password: "user",
  };
  
  export const login = (username, password) => {
    if (username === testUser.username && password === testUser.password) {
      localStorage.setItem("user", JSON.stringify(testUser));
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
  
  export const isAuthenticated = () => {
    return !!getUser();
  };
  