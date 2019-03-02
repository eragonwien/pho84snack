import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { ContactComponent } from "./contact/contact.component";
import { MenuComponent } from "./menu/menu.component";
import { AboutComponent } from "./about/about.component";
import { DrinksComponent } from "./drinks/drinks.component";
import { FoodComponent } from "./food/food.component";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/home" },
  { path: "home", component: HomeComponent },
  { path: "kontakt", component: ContactComponent },
  { path: "menu", component: MenuComponent },
  { path: "essen", component: FoodComponent },
  { path: "trinken", component: DrinksComponent },
  { path: "about", component: AboutComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, scrollPositionRestoration: "top" })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
