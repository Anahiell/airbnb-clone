const API_URL = "https://localhost:5001/api/auth"; // URL API

/** Регистрация пользователя */
export const registerUser = async (email, password) => {
  try {
    const response = await fetch(`${API_URL}/register`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password }),
    });

    const data = await response.json();
    if (!response.ok) throw new Error(data.message || "Ошибка регистрации");

    return data; // { message: "Регистрация успешна" }
  } catch (error) {
    console.error("Ошибка регистрации:", error);
    throw error;
  }
};

/** Авторизация пользователя */
export const loginUser = async (email, password) => {
  try {
    const response = await fetch(`${API_URL}/login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password }),
    });

    const data = await response.json();
    if (!response.ok) throw new Error(data.message || "Ошибка входа");

    localStorage.setItem("token", data.token); // Сохранение токена
    return data; // { token: "JWT-токен" }
  } catch (error) {
    console.error("Ошибка входа:", error);
    throw error;
  }
};

/** Выход из системы */
export const logoutUser = () => {
  localStorage.removeItem("token");
};
