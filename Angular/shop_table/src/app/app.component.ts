import { Component } from '@angular/core';

class ShopItem {
    purchase: string;
    available: string;
    price: number;

    constructor(purchase: string, available: string, price: number) {
        this.purchase = purchase;
        this.price = price;
        this.available = available;
    }
}

class Item {
    purchase: string;
    price: number;
    done: boolean

    constructor(purchase: string, price: number) {
        this.purchase = purchase;
        this.price = price;
        this.done = false;
    }
}

// selector - которое определяет селектор CSS. В элемент с этим селектором Angular будет добавлять представление компонента. 
// template -Шаблон представляет кусок разметки HTML с вкраплениями кода Angular. Фактически шаблон это и есть представление, которое увидит пользователь при работе с приложением.

@Component({
    selector: 'purchase-app',
    templateUrl: `./html/app.component.html`,
    styleUrls: [`./css/app.component.css`]
})

export class AppComponent {

    items: Item[] =
        [
        ];

    shopitems: ShopItem[] =
        [
            { purchase: "Телевизор", price: 310, available: "Есть" },
            { purchase: "Телефон", price: 60, available: "Есть" },
            { purchase: "Ручка", price: 15.9, available: "Есть" },
            { purchase: "Телевизор", price: 310, available: "Есть" },
            { purchase: "Часы", price: 60, available: "Есть" },
            { purchase: "Ручка", price: 44.9, available: "Есть" },
            { purchase: "Стол", price: 312, available: "Есть" },
            { purchase: "Стул", price: 603, available: "Есть" },
            { purchase: "Телевизор", price: 310, available: "Есть" },
            { purchase: "Телефон", price: 60, available: "Есть" },
            { purchase: "Ручка", price: 15.9, available: "Есть" },
            { purchase: "Телевизор", price: 310, available: "Есть" },
            { purchase: "Часы", price: 60, available: "Есть" },
            { purchase: "Ручка", price: 44.9, available: "Есть" },
            { purchase: "Стол", price: 312, available: "Есть" },
            { purchase: "Стул", price: 603, available: "Есть" },
            { purchase: "Телефон", price: 60, available: "Есть" },
            { purchase: "Ручка", price: 15.9, available: "Есть" },
            { purchase: "Телевизор", price: 310, available: "Есть" },
            { purchase: "Часы", price: 60, available: "Есть" },
            { purchase: "Ручка", price: 44.9, available: "Есть" },
            { purchase: "Стол", price: 312, available: "Есть" },
            { purchase: "Стул", price: 603, available: "Есть" },
            { purchase: "Книга", price: 15.9, available: "Нету" }
        ];

    all_price: number = 0;
    all_price_buy: number = 0;
    index: number;

    titleDir = 1;
    priceDir = 1;
    
    switchTitleSort(mass : any[]) : void {
        this.titleDir *= -1;
        mass.sort((a, b) => {
            if (a.purchase > b.purchase) {
                return this.titleDir;
            }

            if (a.purchase < b.purchase) {
                return -this.titleDir;
            }

            return 0;
        })
    }

    switchPriceSort(mass : any[]) : void {
        this.priceDir *= -1;
        mass.sort((a, b) => {
            if (a.price > b.price) {
                return this.priceDir;
            }

            if (a.price < b.price) {
                return -this.priceDir;
            }

            return 0;
        })
    }

    addItem(purchase: string, price: number): void {

        if (purchase == null || purchase.trim() == "" || price == null)
            return;

        this.all_price_buy += price;
        this.items.push(new Item(purchase, price));
    }

    removeItem(purchase: string, price: number, done: boolean): void {

        if (purchase == null || purchase.trim() == "" || price == null)
            return;

        this.index = this.items.indexOf(new Item(purchase, price), )
        this.items.splice(this.index, 1);

        this.all_price_buy -= price;
        if (done == true) {
            this.all_price -= price;
            this.check();
        }
    }

    removeAllItem(): void {

        this.items.length = 0;
        this.all_price_buy = 0;
        this.all_price = 0;
    }

    check(): void {

        if (this.all_price < 0.001 || this.all_price_buy < 0.001) {
            this.all_price == 0;
            this.all_price_buy == 0;
        }
    }

    increase_decrease(price: number, done: boolean): void {

        if (done == true) {
            this.all_price += price;
        }
        else {
            this.all_price -= price;
            this.check();
        }
    }

    findItem(purchase: string) {

        if (purchase == null || purchase.trim() == "")
            return;

        for (var i = 0; i < this.items.length; i++) {
            if (this.items[i] == new Item(purchase, null)) return i;
        }
    }   
}