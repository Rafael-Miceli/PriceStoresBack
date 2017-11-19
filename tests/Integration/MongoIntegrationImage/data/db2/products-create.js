db = db.getSiblingDB('PriceStore')

db.products.save({
    _id : "",
    name : "cebola",
    actualPrice : 2,
    category : {
        id: "",
        name: "Sem Categoria"
    }
});