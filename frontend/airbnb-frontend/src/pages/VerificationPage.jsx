import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const VerificationPage = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [documentType, setDocumentType] = useState("passport");
  const [frontImage, setFrontImage] = useState(null);
  const [backImage, setBackImage] = useState(null);

  useEffect(() => {
    const storedUser = JSON.parse(localStorage.getItem("user"));
    if (!storedUser || !storedUser.profile) {
      navigate("/");
    } else {
      setUser(storedUser);
    }
  }, [navigate]);

  const handleFileChange = (e, side) => {
    const file = e.target.files[0];
    if (file) {
      if (side === "front") {
        setFrontImage(file);
      } else {
        setBackImage(file);
      }
    }
  };

  const handleVerify = () => {
    if (frontImage && backImage) {
      const updatedUser = { ...user, isVerified: true };
      localStorage.setItem("user", JSON.stringify(updatedUser));
      navigate("/profile");
    } else {
      alert("Загрузите оба изображения!");
    }
  };

  if (!user) return <p>Загрузка...</p>;

  return (
    <div>
      <h1>Верификация</h1>
      
      <label>Выберите тип документа</label>
      <select value={documentType} onChange={(e) => setDocumentType(e.target.value)}>
        <option value="passport">Паспорт</option>
        <option value="id">ID-карта</option>
      </select>

      <label>Загрузите лицевую сторону</label>
      <input type="file" accept="image/*" onChange={(e) => handleFileChange(e, "front")} />

      <label>Загрузите обратную сторону</label>
      <input type="file" accept="image/*" onChange={(e) => handleFileChange(e, "back")} />

      <button onClick={handleVerify}>Отправить на проверку</button>
      <button onClick={() => navigate("/profile")}>Назад</button>
    </div>
  );
};

export default VerificationPage;
