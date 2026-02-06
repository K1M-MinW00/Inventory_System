# 🧳 Inventory & Shop System (Unity)

Unity 기반 게임에서 사용 가능한 확장형 인벤토리 / 상점 시스템입니다.

아이템 데이터 관리부터 플레이어 핫바, 인벤토리, 상점 거래, 저장/로드까지 구현하였습니다.

<br>

## 📌 프로젝트 개요

Engine: Unity 6.0

Language: C#

Input System: Unity Input System

Data Structure: ScriptableObject / Serializable Data

Target: 3D RPG / Action / Survival 게임 공용 인벤토리 구조

<br>

## ✨ 주요 기능 요약

- ScriptableObject 기반 아이템 데이터 시스템
- 스택형 인벤토리 로직
- Input System 기반 플레이어 핫바 선택 및 아이템 사용
- 상점 Buy / Sell 거래 시스템
- 장바구니 기반 거래 검증
- 상점 상태 저장 / 로드
- UI ↔ 데이터 분리 구조

<br>

---

### 📦 Item System
ItemData (ScriptableObject)

- 아이템 고유 ID 기반 관리
- 아이콘 / 설명 / 가격 / 최대 스택 수 정의

📷 Item SO

<img width="450" height="271" alt="image" src="https://github.com/user-attachments/assets/d426bcf5-8d86-42ce-a993-0831f9aaaf1e" />

---

### 🎒 Inventory System

- 슬롯 기반 인벤토리 구조
- 동일 아이템 자동 스택 병합
- 빈 슬롯 우선 탐색
- 여러 슬롯에 걸친 아이템 제거 지원
- 인벤토리 변경 이벤트 제공 (OnInventorySlotChanged)

### 🎯 Player Hotbar System

- 숫자 키(0~9)로 슬롯 직접 선택
- 마우스 휠로 핫바 순환 이동
- 현재 선택 슬롯 하이라이트
- 선택 슬롯의 아이템 사용 (UseItem() 디버그 호출)

📷 핫바 선택 & 사용 / 인벤토리 UI

<img width="450" height="300" alt="image" src="https://github.com/user-attachments/assets/977a54d9-745a-4f82-96b0-afb33c239480" />

---

### 🏪 Shop System

- 상점 전용 슬롯 시스템
- 아이템 스택 관리
- 슬롯 부족 시 자동 확장
- 가격 정책 (Buy / Sell 마크업 분리)
- 아이템 단가 계산 공용 메서드 사용

📷 상점 UI (Buy)

<img width="450" height="300" alt="image" src="https://github.com/user-attachments/assets/dc5bb12e-a0ee-4fa2-ac05-b2f077df79cc" />

<br>

### 🛒 Shopping Cart 시스템

- 아이템 단위 장바구니 관리
- 해당 아이템 프리뷰
- 수량 증감 가능
- 총 금액 실시간 계산
- 골드 부족 / 인벤토리 부족 시 UI 피드백

📷 상점 UI (Sell)

<img width="450" height="300" alt="image" src="https://github.com/user-attachments/assets/8153bbb1-74cc-4058-8a1c-b4bbf8973d40" />

<br>

### 🔄 Buy / Sell 거래 플로우

1. 구매 (Buy)
```
- 플레이어 골드 확인
   ↓
- 인벤토리 공간 검증
   ↓
- 상점 아이템 감소
   ↓
- 플레이어 인벤토리 추가
   ↓
- 골드 정산
```

2. 판매 (Sell)
```
- 상점 골드 확인
   ↓
- 플레이어 아이템 감소
   ↓
- 상점 아이템 증가
   ↓
- 골드 정산
```

---

### 📦 Chest / Container System

- 상호기반 작용 가능한 상자 오브젝트
- 상자별 독립적인 사이즈의 인벤토리 시스템
- 플레이어 인벤토리와 동일한 슬롯 / 스택 구조 재사용
- UniqueID 기반 상자 상태 저장 및 복원
- UI ↔ 데이터 완전 분리 구조

📷 상자 UI 화면

<img width="450" height="300" alt="image" src="https://github.com/user-attachments/assets/0458c8b9-82c8-438d-bdf5-7f9a4d2b6896" />

---

### 📚 Reference & Acknowledgement

This project was initially inspired by an inventory system tutorial by **[DanPos](https://www.youtube.com/@DanPos)**.
