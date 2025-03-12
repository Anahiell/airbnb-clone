const API_URL = "https://localhost:5001/api/profile"; // Бэкенд API

/** Получить данные профиля */
export const getProfile = async () => {
  try {
    const response = await fetch(`${API_URL}/me`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });

    const data = await response.json();
    if (!response.ok) throw new Error(data.message || "Ошибка получения профиля");

    return data; // { fullName, location, birthYear, ... }
  } catch (error) {
    console.error("Ошибка профиля:", error);
    throw error;
  }
};

/** Обновить данные профиля */
export const updateProfile = async (profileData) => {
  try {
    const response = await fetch(`${API_URL}/update`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(profileData),
    });

    const data = await response.json();
    if (!response.ok) throw new Error(data.message || "Ошибка обновления");

    return data; // { message: "Профиль обновлен" }
  } catch (error) {
    console.error("Ошибка обновления профиля:", error);
    throw error;
  }
};

/** Загрузить фото профиля */
export const uploadAvatar = async (file) => {
  try {
    const formData = new FormData();
    formData.append("avatar", file);

    const response = await fetch(`${API_URL}/upload-avatar`, {
      method: "POST",
      body: formData,
    });

    const data = await response.json();
    if (!response.ok) throw new Error(data.message || "Ошибка загрузки");

    return data; // { avatarUrl: "https://..." }
  } catch (error) {
    console.error("Ошибка загрузки аватара:", error);
    throw error;
  }
};
