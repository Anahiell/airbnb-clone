import { useState } from "react";
import PropertyCard from "../components/PropertyCard";
import CategoryFilters from "../components/CategoryFilters";
import propertiesData from "../data/properties"; // ✅ Импортируем данные
import styles from "../styles/HomePage.module.css";

const HomePage = () => {
  const [properties, setProperties] = useState(propertiesData);
  const [selectedCategory, setSelectedCategory] = useState("all");

 

  const handleCategorySelect = (category) => {
    setSelectedCategory(category);
    let filtered = category === "all" ? propertiesData : propertiesData.filter((p) => p.category === category);
    setProperties(filtered);
  };

  return (
    <div className={styles.home}>
      <h1 className={styles.title}>Найдите жилье для отпуска</h1>
      
      <CategoryFilters selectedCategory={selectedCategory} onSelectCategory={handleCategorySelect} />

      <div className={styles.grid}>
        {properties.length > 0 ? (
          properties.map((property) => <PropertyCard key={property.id} {...property} />)
        ) : (
          <p className={styles.noResults}>Ничего не найдено 😞</p>
        )}
      </div>
    </div>
  );
};

export default HomePage;
