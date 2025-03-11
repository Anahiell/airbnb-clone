/** Получить токен */
export const getToken = () => localStorage.getItem("token");

/** Удалить токен */
export const removeToken = () => localStorage.removeItem("token");
