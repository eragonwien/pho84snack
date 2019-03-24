import { Component, OnInit } from "@angular/core";
import { Category } from "../models";
import { DataService } from "../services/data.service";

@Component({
  selector: "app-drinks",
  templateUrl: "./drinks.component.html",
  styleUrls: ["./drinks.component.css"]
})
export class DrinksComponent implements OnInit {
  categories: Category[];

  constructor(private ds: DataService) {}

  ngOnInit() {
    this.ds.category.subscribe((categories: Category[]) => {
      this.categories = categories.filter(c => c.type === "drink");
    });
  }
}
