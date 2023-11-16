import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameLoadingComponent } from './game-loading.component';

describe('GameLoadingComponent', () => {
  let component: GameLoadingComponent;
  let fixture: ComponentFixture<GameLoadingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GameLoadingComponent]
    });
    fixture = TestBed.createComponent(GameLoadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
